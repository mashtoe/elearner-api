using System.Collections.Generic;
using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.ApplicationService
{
    public interface ICategoryService
    {
        CategoryBO Create(CategoryBO category);
        CategoryBO Get(int id);
        //returns all sections (CRUD: Read)
        List<CategoryBO> GetAll();
        //Update  (CRUD: Update)
        CategoryBO Update(CategoryBO category);
        //Delete one section with id (CRUD: Delete)
        CategoryBO Delete(int id);
    }
}