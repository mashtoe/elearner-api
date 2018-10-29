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
                Students = course.StudentIds?.Select(sId => new StudentCourse() {
                    CourseId = course.Id,
                    StudentId = sId
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
                StudentIds = course.Students?.Select(s => s.StudentId).ToList()
            };

        }
    }
}
