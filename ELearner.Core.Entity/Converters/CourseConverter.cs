using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Core.Entity.Converters {
    public class CourseConverter {
        public Course Convert(CourseBO course) {
            if (course == null) {
                return null;
            }
            return new Course() {
                Id = course.Id,
                Name = course.Name,
                Users = course.UserIds?.Select(sId => new UserCourse() {
                    CourseId = course.Id,
                    UserID = sId
                }).ToList()
            };
        }

        public CourseBO Convert(Course course) {
            if (course == null) {
                return null;
            }
            return new CourseBO() {
                Id = course.Id,
                Name = course.Name,
                UserIds = course.Users?.Select(s => s.UserID).ToList()
            };

        }
    }
}
