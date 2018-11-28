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
        //Update  (CRUD: Update)
        ApplicationBO Update(ApplicationBO application);
        //Delete one lesson with id (CRUD: Delete)
        ApplicationBO Delete(int id);
    }
}