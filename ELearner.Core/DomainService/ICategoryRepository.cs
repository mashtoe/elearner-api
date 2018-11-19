using System.Collections.Generic;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.DomainService
{
    public interface ICategoryRepository
    {
        //Create Data
        //No Id on Enter, but Id on exit
        Category Create(Category category);
        //Read Data
        Category Get(int id);
        IEnumerable<Category> GetAll();
        //Delete Data
        Category Delete(int id);
        IEnumerable<Category> GetAllById(IEnumerable<int> ids);
    }
}