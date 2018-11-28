using System.Collections.Generic;
using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.ApplicationService
{
    public interface IApplicationService
    {
        ApplicationBO Create(ApplicationBO application);
        ApplicationBO Get(int id);
        //returns all lessons (CRUD: Read)
        List<ApplicationBO> GetAll();
        // we dont need update
        //Delete one lesson with id (CRUD: Delete)
        ApplicationBO Delete(int id);
    }
}