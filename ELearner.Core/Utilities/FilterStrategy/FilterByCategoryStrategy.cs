using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterByCategoryStrategy : IFilterStrategy {
        public int? CategoryId { get; set; }
        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            if (CategoryId != null) {
                courses = courses.Where(c => c.CategoryId == CategoryId);
            }
            return courses;
        }
    }
}
