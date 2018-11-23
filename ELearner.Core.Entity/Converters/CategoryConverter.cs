using System.Linq;
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
                Name = category.Name,
                Courses = category.CourseIds?.Select(cId => new Course()
                {
                    CategoryId = category.Id
                }).ToList()

                
            };
        }

        public CategoryBO Convert(Category category)
        {
            var crsConv = new CourseConverter();
            if (category == null)
            {
                return null;
            }
            return new CategoryBO(){
                Id = category.Id,
                Name = category.Name,
                Courses = category.Courses.Select(c => crsConv.Convert(c)).ToList()
                //CourseIds = category.Courses?.Select(c => c.Id).ToList()
            };
        }
    }
}
