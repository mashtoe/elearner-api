using System.Collections.Generic;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.DomainService
{
    public interface IApplicationRepository
    {
        //Create Data
        //No Id on Enter, but Id on exit
        Application Create(Application application);
        //Read Data
        Application Get(int id);
        IEnumerable<Application> GetAll();
        //Delete Data
        Application Delete(int id);
        IEnumerable<Application> GetAllById(IEnumerable<int> ids);
    }
}