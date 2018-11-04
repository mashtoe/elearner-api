using System;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services
{
    public class AuthService : IAuthService
    {
        readonly UserConverter _userConv;
        readonly IUnitOfWork _uow;
        public AuthService(IUnitOfWork uow)
        {
            _userConv = new UserConverter();
            _uow = uow;
        }
        public UserBO Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public UserBO Register(UserBO user, string password)
        {
            using (_uow)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                var userRegistered = _uow.UserRepo.Create(_userConv.Convert(user));

                _uow.Complete();
                return _userConv.Convert(userRegistered);
            }

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public bool UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}