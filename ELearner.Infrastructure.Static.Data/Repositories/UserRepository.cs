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
                    _fakeDb.UserCourses.Add(item);
                }
            }
            user.Courses = null;
            _fakeDb.Users.Add(user);
            return user;
        }

        public User Get(int id) {
            var user = _fakeDb.Users.FirstOrDefault(s => s.Id == id);
            // include course ids. In EF we would use Include() method, but here we are using a fake db consisting of lists only,
            // but we have to return the same properties that are returned in the other implementations of the infrastructure layer

            List<UserCourse> courses = null;
            if (user != null) {
                courses = _fakeDb.UserCourses.Where(sc => sc.UserID == id).ToList();
            }
            //return new object to avoid messing with the objects in the fake db
            return new User() {
                Username = user.Username,
                Courses = courses
            };
        }

        public IEnumerable<User> GetAll() {
            return _fakeDb.Users;
        }

        public User Update(User user) {
            var userFromDb = Get(user.Id);
            if (userFromDb == null) return null;

            userFromDb.Username = user.Username;
            userFromDb.UserRole = user.UserRole;
            return userFromDb;
        }

        public User Delete(int id) {
            var userFromDb = Get(id);
            if (userFromDb == null) return null;
            _fakeDb.Users.Remove(userFromDb);
            return userFromDb;
        }

        public IEnumerable<User> GetAllById(IEnumerable<int> ids) {
            var users = _fakeDb.Users.Where(s => ids.Contains(s.Id));
            return users;
        }
    }
}
