using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterEnrolledStrategy : IFilterStrategy {
        //public List<UserCourse> UserCourses { get; set; }
        public User User { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            if (User != null) {
                courses = courses.Where(c => User.Courses.Exists(uc => uc.CourseId == c.Id));
            }
            return courses;
        }
    }   
}
