using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Repositories {

    // the implementaion of the repository interfaces are part of the Infrastructure layer, which is an outer layer of the onion
    // the implementaions of the repository interfaces are decoupled & undepenable so that they are easily interchangeable & editable
    public class UserRepository : IUserRepository {

        readonly FakeDB _fakeDb;

        public UserRepository(FakeDB fakeDb) {
            _fakeDb = fakeDb;
        }

        public User Create(User user) {
            user.Id = FakeDB.Id++;

            if (user.Courses != null) {
                // adding the reference between objects in the fake db
                foreach (var item in user.Courses) {
                    item.UserID = user.Id;
                    _fakeDb.UserCoursesNotSaved.Add(item);
                }
            }
            user.Courses = null;
            _fakeDb.UsersNotSaved.Add(user);
            return user;
        }

        public User Get(int id) {
            var user = _fakeDb.UsersNotSaved.FirstOrDefault(s => s.Id == id);
            // include course ids. In EF we would use Include() method, but here we are using a fake db consisting of lists only,
            // but we have to return the same properties that are returned in the other implementations of the infrastructure layer

            List<UserCourse> courses = null;
            if (user != null) {
                courses = _fakeDb.UserCoursesNotSaved.Where(sc => sc.UserID == id).ToList();
                user.Courses = courses;
            }
            //return new object to avoid messing with the objects in the fake db
            return user;
            /*return new User() {
                Username = user.Username,
                Courses = courses
            };*/
        }

        public IEnumerable<User> GetAll() {
            return _fakeDb.UsersNotSaved;
        }

        public User Delete(int id) {
            var userFromDb = Get(id);
            if (userFromDb == null) return null;
            var referencesToRemove = _fakeDb.UserCoursesNotSaved.Where(uc => uc.UserID == id).ToList();
            int count = referencesToRemove.Count();
            for (int i = 0; i < count; i++) {
                _fakeDb.UserCoursesNotSaved.Remove(referencesToRemove[i]);
            }
            _fakeDb.UsersNotSaved.Remove(userFromDb);
            return userFromDb;
        }

        public IEnumerable<User> GetAllById(IEnumerable<int> ids) {
            var users = _fakeDb.UsersNotSaved.Where(s => ids.Contains(s.Id));
            return users;
        }
    }
}
