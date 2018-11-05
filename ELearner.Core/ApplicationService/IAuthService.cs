using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;

namespace ELearner.Core.ApplicationService
{
    public interface IAuthService
    {
        UserBO Register(UserRegisterDto userDto);
        UserBO Login(string username, string password);
        bool UserExists(string username);
    }
}