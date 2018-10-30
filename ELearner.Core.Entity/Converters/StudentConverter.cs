using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Core.Entity.Converters {
    public class StudentConverter {
        public Student Convert(StudentBO student) {
            if (student == null) {
                return null;
            }
            var converted = new Student() {
                Id = student.Id,
                Username = student.Username,
                Courses = student.CourseIds?.Select(cId => new StudentCourse() {
                    CourseId = cId,
                    StudentId = student.Id
                }).ToList()
            };
            return converted;
        }

        public StudentBO Convert(Student student) {
            if (student == null) {
                return null;
            }
            return new StudentBO() {
                Id = student.Id,
                Username = student.Username,
                CourseIds = student.Courses?.Select(c => c.CourseId).ToList()
            };

        }
    }
}
