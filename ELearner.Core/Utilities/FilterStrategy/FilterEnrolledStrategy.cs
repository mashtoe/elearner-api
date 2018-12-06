using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterEnrolledStrategy : IFilterStrategy {
        public List<UserCourse> UserCourses { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            if (UserCourses != null) {
                courses = courses.Where(c => UserCourses.Exists(uc => uc.CourseId == c.Id));
                var list = courses.ToList();
                //courses = courses.Where(c => c.Users.Exists(u => u.UserID == User.Id));
            }
            return courses;
        }
    }   
}
