using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterByOnlyPublishedStrategy : IFilterStrategy {
        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Where(c => c.Published == true);
            return courses;
        }
    }
}
