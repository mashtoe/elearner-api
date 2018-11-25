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
        readonly ISectionService _secService;
        readonly ICategoryService _catService;
        readonly ILessonService _lesService;


        public DataSeeder(IAuthService authService, ICourseService courseService, IUserService userService, ICategoryService categoryService, ISectionService sectionService, ILessonService lessonService) {
            _authService = authService;
            _courseService = courseService;
            _userService = userService;
            _catService = categoryService;
            _secService = sectionService;
            _lesService = lessonService;
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

            _userService.Promote(_userService.Promote(_userService.Promote(_authService.Register(admin).Id).Id).Id);
            List<int> userIds = new List<int>();
            userIds.Add(userCreated.Id);

            var category = new CategoryBO(){
                Name = "Math"
            };
            

            var favCategory = _catService.Create(category);

            var course = new CourseBO() {
                Name = " Building Course",
                UserIds = userIds,
                CategoryId = favCategory.Id
            };
            _courseService.Create(course);

            var section = new SectionBO()
            {
                Title = "Hard stuff",
                CourseId = 1
            };
            var hardSection = _secService.Create(section);

            var lesson = new LessonBO()
            {
                Title = "Introduction to learning all the cool stuff",
                SectionId = 1
            };

            var firstLesson = _lesService.Create(lesson);

            for (int i = 0; i < 50; i++) {
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
                    UserIds = userIds,
                    CategoryId = favCategory.Id
                };
                _courseService.Create(crs);
            }
        }
    }
}
