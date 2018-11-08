using ELearner.Core.ApplicationService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Utilities {
    public class DataSeeder : IDataSeeder {

        readonly IAuthService _authService;
        readonly ICourseService _courseService;

        public DataSeeder(IAuthService authService, ICourseService courseService) {
            _authService = authService;
            _courseService = courseService;
        }

        public void SeedData() {
            var user1 = new UserRegisterDto() {
               Username = "BoringManOG",
               Password = "secretpassword"
            };
            _authService.Register(user1);

            var user2 = new UserRegisterDto() {
                Username = "FunnyMan2",
                Password = "verysecret"
            };
            var userCreated = _authService.Register(user2);

            List<int> userIds = new List<int>();
            userIds.Add(userCreated.Id);
            var course = new CourseBO() {
                Name = " Building Course",
                UserIds = userIds
            };
            _courseService.Create(course);

            for (int i = 0; i < 100; i++) {
                if (i % 10 == 0) {
                    var crs2 = new CourseBO() {
                        Name = " A Course" + i,
                    };
                    _courseService.Create(crs2);
                }
                var crs = new CourseBO() {
                    Name = " Course" + i,
                };
                _courseService.Create(crs);
            }
        }
    }
}
