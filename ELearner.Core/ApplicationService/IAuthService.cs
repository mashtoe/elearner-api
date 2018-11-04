using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.ApplicationService
{
    public interface IAuthService
    {
        UserBO Register(UserBO user, string password);
        UserBO Login(string username, string password);
        bool UserExists(string username);
    }
}