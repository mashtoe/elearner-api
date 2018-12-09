using System.Collections.Generic;
using System.Linq;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterSearchStrategy : IFilterStrategy {

        public string Query { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Where(c => 
            c.Name.ToLower().Contains(Query.ToLower()) || 
            c.Description.ToLower().Contains(Query.ToLower()) ||
            c.Creator.Username.ToLower().Contains(Query.ToLower()));
            return courses;
        }
    }
}
