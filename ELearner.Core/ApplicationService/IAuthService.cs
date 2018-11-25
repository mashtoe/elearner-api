using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;

namespace ELearner.Core.ApplicationService
{
    public interface IAuthService
    {
        UserBO Register(UserRegisterDto userDto);
        //String to return is a Jwt
        string Login(UserLoginDto userDto);
        bool UserExists(string username);
    }
}