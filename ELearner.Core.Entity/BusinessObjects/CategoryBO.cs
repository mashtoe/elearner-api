using System.Collections.Generic;

namespace ELearner.Core.Entity.BusinessObjects
{
    public class CategoryBO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CourseBO> Courses { get; set; }
        public List<int> CourseIds { get; set; }
        
    }
}