using System;
using System.Linq;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;

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

        public UserBO Register(UserRegisterDto userDto)
        {
            using (_uow)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                var userEntity = new User() {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Username = userDto.Username
                };
                var userRegistered = _uow.UserRepo.Create(userEntity);

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
            using (_uow)
            {
                var userExists = _uow.UserRepo.GetAll().Any(
                    user => user.Username == username);
                    return userExists;  
            }
        }
    }
}