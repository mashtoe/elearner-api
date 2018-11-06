using System;
using System.Linq;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities;

namespace ELearner.Core.ApplicationService.Services
{
    public class AuthService : IAuthService
    {
        readonly UserConverter _userConv;
        readonly IUnitOfWork _uow;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthService(IUnitOfWork uow, ITokenGenerator tokenGenerator)
        {
            _userConv = new UserConverter();
            _uow = uow;
            _tokenGenerator = tokenGenerator;
        }
        public string Login(UserLoginDto userDto)
        {
            using (_uow) 
            {
                var userFromDB = _uow.UserRepo.GetAll().FirstOrDefault(
                    user => user.Username.ToLower() == userDto.Username.ToLower()
                );

                if (userFromDB == null)
                {
                    return "";
                }

                if (!VerifyPasswordHash(userDto.Password, userFromDB.PasswordHash, userFromDB.PasswordSalt ))
                {
                    return "";
                }
                return _tokenGenerator.GenerateToken(_userConv.Convert(userFromDB));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public UserBO Register(UserRegisterDto userDto)
        {
            using (_uow)
            {
                if (UserExists(userDto.Username))
                {
                    return null;
                }
                return CreateNewUser(userDto);
            }
        }

        private UserBO CreateNewUser(UserRegisterDto userDto)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            var userEntity = new User()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = userDto.Username
            };
            var userRegistered = _uow.UserRepo.Create(userEntity);

            _uow.Complete();
            return _userConv.Convert(userRegistered);
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
                    var userExists = _uow.UserRepo.GetAll().Any(
                    user => user.Username.ToLower() == username.ToLower());
                    return userExists;     
        }
    }
}