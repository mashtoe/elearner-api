using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Core.Utilities.FilterStrategy {
    class FilterByCreatorStrategy: IFilterStrategy {
        public int CreatorId { get; set; }
        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Where(c => c.CreatorId == CreatorId);
            return courses;
        }
    }
}
