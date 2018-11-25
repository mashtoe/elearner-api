using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.Utilities
{
    public interface ITokenGenerator
    {
         string GenerateToken(UserBO user);
    }
}