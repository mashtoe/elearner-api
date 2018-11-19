using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Entity.Converters
{
    public class CategoryConverter
    {
        public Category Convert(CategoryBO category)
        {
            if (category == null)
            {
                return null;
            }
            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public CategoryBO Convert(Category category)
        {
            if (category == null)
            {
                return null;
            }
            return new CategoryBO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}