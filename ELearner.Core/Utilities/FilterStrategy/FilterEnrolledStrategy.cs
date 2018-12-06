using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterEnrolledStrategy : IFilterStrategy {
        public User User { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            if (User != null) {
                courses = courses.Where(c => true);
                var list = courses.ToList();
                //courses = courses.Where(c => c.Users.Exists(u => u.UserID == User.Id));
            }
            return courses;
        }
    }
}
