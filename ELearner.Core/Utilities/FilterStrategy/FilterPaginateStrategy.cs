using System.Collections.Generic;
using System.Linq;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterPaginateStrategy : IFilterStrategy {

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);
            return courses;
        }
    }
}
