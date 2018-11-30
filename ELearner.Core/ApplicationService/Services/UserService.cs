using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
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

        public UserBO Delete(int id) {
            using (var uow = _facade.UnitOfWork) {
                var userDeleted = uow.UserRepo.Delete(id);
                if (userDeleted == null) {
                    return null;
                }
                uow.Complete();
                return _userConv.Convert(userDeleted);
            }
        }

        public UserBO Get(int id) {
            using (var uow = _facade.UnitOfWork) {
                var user = _userConv.Convert(uow.UserRepo.Get(id));
                if (user != null) {
                    if (user.CourseIds != null) {
                        user.Courses = uow.CourseRepo.GetAllById(user.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
                    }
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
                var userFromDb = uow.UserRepo.Get(user.Id);
                if (userFromDb == null) {
                    return null;
                }
                userFromDb.Username = user.Username;
                uow.Complete();
                return _userConv.Convert(userFromDb);
            }
        }

        public UserBO Promote(int id) {
            using (var uow = _facade.UnitOfWork) {
                var userFromDb = uow.UserRepo.Get(id);
                if ((int)userFromDb.Role < 2) {
                    userFromDb.Role++;
                } else {
                    return null;
                }   
                uow.Complete();
                return _userConv.Convert(userFromDb);
            }
        }
    }
}
