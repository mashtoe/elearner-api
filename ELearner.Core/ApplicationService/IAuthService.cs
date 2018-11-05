using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;

namespace ELearner.Core.ApplicationService
{
    public interface IAuthService
    {
        UserBO Register(UserRegisterDto userDto);
        UserBO Login(UserLoginDto userDto);
        bool UserExists(string username);
    }
}