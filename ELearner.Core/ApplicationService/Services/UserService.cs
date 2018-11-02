using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class UserService : IUserService {

        readonly UserConverter _userConv;
        readonly CourseConverter _crsConv;
        readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow) {
            _userConv = new UserConverter();
            _crsConv = new CourseConverter();
            _uow = uow;
        }
        public UserBO New() {
            return new UserBO();
        }

        public UserBO Create(UserBO user) {
            // TODO check if entity is valid, and throw errors if not
            using (_uow)
            {
                //var studentCreated = uow.StudentRepo.Create(_studConv.Convert(student));
                //student.Username += "1";
                //if (studentCreated != null) {
                //    throw new InvalidOperationException();
                //}
                var userCreated = _uow.UserRepo.Create(_userConv.Convert(user));

                _uow.Complete();
                return _userConv.Convert(userCreated);
            }
        }

        public UserBO Delete(int id) {
            using (_uow) {
                var userDeleted = _uow.UserRepo.Delete(id);
                _uow.Complete();
                return _userConv.Convert(userDeleted);
            }
        }

        public UserBO Get(int id) {
            using (_uow) {
                var user = _userConv.Convert(_uow.UserRepo.Get(id));
                if (user != null) {
                    user.Courses = _uow.CourseRepo.GetAllById(user.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
                }
                return user;
            }
        }

        public List<UserBO> GetAll() {
            using (_uow) {
                var users = _uow.UserRepo.GetAll();
                return users.Select(s => _userConv.Convert(s)).ToList();
            }
        }

        public UserBO Update(UserBO user) {
            using (_uow) {
                var updatedUser = _uow.UserRepo.Update(_userConv.Convert(user));
                _uow.Complete();
                return _userConv.Convert(updatedUser);
            }
        }
    }
}
