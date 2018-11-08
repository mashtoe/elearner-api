using System.Collections.Generic;
using System.Linq;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterSearchStrategy : IFilterStrategy {

        public string Query { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Where(c => c.Name.Contains(Query));
            return courses;
        }
    }
}
