using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly ElearnerAppContext _context;
        public CategoryRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public Category Create(Category category)
        {
            var categoryFromDB = _context.Categories.Add(category).Entity;
            return categoryFromDB;
        }
        public Category Get(int id)
        {
            return _context.Categories
            .Include(c => c.Courses)
            .FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }
        public IEnumerable<Category> GetAllById(IEnumerable<int> ids)
        {
            if (ids == null)
                return null;

            return _context.Categories.Where(c => ids.Contains(c.Id));
        }
        public Category Delete(int id)
        {
            var categoryRemoved = Get(id);
            if (categoryRemoved == null) return null;
            _context.Remove(categoryRemoved);
            return categoryRemoved;
        }
    }
}