using System.Collections.Generic;
using System.Linq;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterSearchStrategy : IFilterStrategy {

        public string Query { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            courses = courses.Where(c => 
            NameContainsQuery(c) || 
            DescriptionContainsQuery(c) ||
            CreatorUsernameContainsQuery(c) );
            return courses;
        }

        private bool NameContainsQuery(Course course) {
            if (course.Name != null) {
                return course.Name.ToLower().Contains(Query.ToLower());
            }
            return false;
        }

        private bool DescriptionContainsQuery(Course course) {
            if (course.Description != null) {
                return course.Description.ToLower().Contains(Query.ToLower());
            }
            return false;
        }

        private bool CreatorUsernameContainsQuery(Course course) {
            if (course.Description != null) {
                return course.Creator.Username.ToLower().Contains(Query.ToLower());
            }
            return false;
        }
    }
}
