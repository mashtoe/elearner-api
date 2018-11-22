using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services
{
    public class CategoryService : ICategoryService
    {
        readonly CategoryConverter _categoryConv;
        readonly CourseConverter _crsConv;
        readonly IDataFacade _facade;
        public CategoryService(IDataFacade facade)
        {
            _categoryConv = new CategoryConverter();
            _crsConv = new CourseConverter();
            _facade = facade;
        }

        public CategoryBO Create(CategoryBO category)
        {
            using (var uow = _facade.UnitOfWork)
            {
                // TODO check if entity is valid, and throw errors if not
                var categoryCreated = uow.CategoryRepo.Create(_categoryConv.Convert(category));
                uow.Complete();
                return _categoryConv.Convert(categoryCreated);
            }
        }
        public CategoryBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var category = _categoryConv.Convert(uow.CategoryRepo.Get(id));
                if (category != null)
                {
                    category.Course = _crsConv.Convert(uow.CourseRepo.Get(category.CourseId));
                }
                uow.Complete();
                return category;
            }
        }
        public List<CategoryBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                var categories = uow.CategoryRepo.GetAll();
                return categories.Select(c => _categoryConv.Convert(c)).ToList();
            }
        }

        public CategoryBO Update(CategoryBO category)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var categoryFromDb = uow.CategoryRepo.Get(category.Id);
                if (categoryFromDb == null)
                {
                    return null;
                }
                categoryFromDb.Name = category.Name;
                categoryFromDb.CourseId = category.CourseId;
                uow.Complete();
                return _categoryConv.Convert(categoryFromDb);
            }
        }


        public CategoryBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var categoryToDelete = Get(id);
                if (categoryToDelete == null)
                {
                    return null;
                }
                categoryToDelete = _categoryConv.Convert(uow.CategoryRepo.Delete(id));
                uow.Complete();
                return categoryToDelete;
            }
        }
    }
}