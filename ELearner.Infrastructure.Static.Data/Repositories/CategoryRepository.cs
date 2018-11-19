using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;

namespace ELearner.Infrastructure.Static.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly FakeDB _fakeDB;
        public CategoryRepository(FakeDB fakeDB)
        {
            _fakeDB = fakeDB;
        }
        public Category Create(Category category)
        {
            category.Id = FakeDB.Id++;

            _fakeDB.CategoriesNotSaved.Add(category);
            return category;
        }

        public Category Get(int id)
        {
            var category = _fakeDB.CategoriesNotSaved.FirstOrDefault(c => c.Id == id);
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return _fakeDB.CategoriesNotSaved;
        }

        public IEnumerable<Category> GetAllById(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }
        public Category Delete(int id)
        {
            var CategoryFromDb = Get(id);
            if (CategoryFromDb == null) return null;

            _fakeDB.CategoriesNotSaved.Remove(CategoryFromDb);
            return CategoryFromDb;
        }
    }
}