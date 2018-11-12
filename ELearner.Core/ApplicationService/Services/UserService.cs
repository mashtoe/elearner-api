using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class UserService : IUserService {

        readonly UserConverter _userConv;
        readonly CourseConverter _crsConv;
        readonly IDataFacade _facade;

        public UserService(IDataFacade facade) {
            _userConv = new UserConverter();
            _crsConv = new CourseConverter();
            _facade = facade;
        }
        public UserBO New() {
            return new UserBO();
        }

        public UserBO Create(UserBO user) {
            // TODO check if entity is valid, and throw errors if not
            using (var uow = _facade.UnitOfWork)
            {
                //var studentCreated = uow.StudentRepo.Create(_studConv.Convert(student));
                //student.Username += "1";
                //if (studentCreated != null) {
                //    throw new InvalidOperationException();
                //}
                var userCreated = uow.UserRepo.Create(_userConv.Convert(user));

                uow.Complete();
                return _userConv.Convert(userCreated);
            }
        }

        public UserBO Delete(int id) {
            using (var uow = _facade.UnitOfWork) {
                var userDeleted = uow.UserRepo.Delete(id);
                uow.Complete();
                return _userConv.Convert(userDeleted);
            }
        }

        public UserBO Get(int id) {
            using (var uow = _facade.UnitOfWork) {
                var user = _userConv.Convert(uow.UserRepo.Get(id));
                if (user != null) {
                    user.Courses = uow.CourseRepo.GetAllById(user.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
                }
                return user;
            }
        }

        public List<UserBO> GetAll() {
            using (var uow = _facade.UnitOfWork) {
                var users = uow.UserRepo.GetAll();
                return users.Select(s => _userConv.Convert(s)).ToList();
            }
        }

        public UserBO Update(UserBO user) {
            using (var uow = _facade.UnitOfWork) {
                var updatedUser = uow.UserRepo.Update(_userConv.Convert(user));
                uow.Complete();
                return _userConv.Convert(updatedUser);
            }
        }

        public UserBO Promote(int id) {
            using (var uow = _facade.UnitOfWork) {
                var userFromDb = uow.UserRepo.Get(id);
                if ((int)userFromDb.Role < 2) {
                    userFromDb.Role++;
                }
                uow.Complete();
                return _userConv.Convert(userFromDb);
            }
        }
    }
}
