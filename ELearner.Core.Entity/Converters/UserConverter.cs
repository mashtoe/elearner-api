using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Core.Entity.Converters {
    public class UserConverter {
        public User Convert(UserBO user) {
            if (user == null) {
                return null;
            }
            var converted = new User() {
                Id = user.Id,
                Username = user.Username,
                Courses = user.CourseIds?.Select(cId => new UserCourse() {
                    CourseId = cId,
                    UserID = user.Id
                }).ToList(),
                Role = user.Role,
                ApplicationId = user.ApplicationId
            };
            return converted;
        }

        public UserBO Convert(User user) {
            if (user == null) {
                return null;
            }
            return new UserBO() {
                Id = user.Id,
                Username = user.Username,
                CourseIds = user.Courses?.Select(c => c.CourseId).ToList(),
                Role = user.Role,
                ApplicationId = user.ApplicationId
            };
        }


    }
}
