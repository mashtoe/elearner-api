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
        readonly IUserService _userService;


        public DataSeeder(IAuthService authService, ICourseService courseService, IUserService userService) {
            _authService = authService;
            _courseService = courseService;
            _userService = userService;
        }

        public void SeedData() {
            var user = new UserRegisterDto() {
               Username = "UserMan",
               Password = "secretpassword"
            };
            var userCreated = _authService.Register(user);

            var educator = new UserRegisterDto() {
                Username = "EducatorMan",
                Password = "secretpassword"
            };
            _userService.Promote(_authService.Register(educator).Id);

            var admin = new UserRegisterDto() {
                Username = "AdminMan",
                Password = "secretpassword"
            };
            ;
            _userService.Promote(_userService.Promote(_userService.Promote(_authService.Register(admin).Id).Id).Id);
            List<int> userIds = new List<int>();
            userIds.Add(userCreated.Id);
            var course = new CourseBO() {
                Name = " Building Course",
                UserIds = userIds
            };
            //_courseService.Create(course);

            for (int i = 0; i < 0; i++) {
                /*
                if (i % 10 == 0) {
                    var crs2 = new CourseBO() {
                        Name = " A Course" + i,
                    };
                    _courseService.Create(crs2);
                }
                */
                var crs = new CourseBO() {
                    Name = " Course" + i,
                    UserIds = userIds
                };
                _courseService.Create(crs);
            }
        }
    }
}
