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
            #region User creation
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
            #endregion

            var favCategory = _catService.Create(category);

            #region Building course
            var lessons = new List<LessonBO>();
            for (int i = 0; i < 20; i++) {
                var lesson = new LessonBO() {
                    Title = "Hello" + i,
                    VideoId = "dogs.mp4"
                };
                lessons.Add(lesson);

            }
            var section = new SectionBO() {
                Title = "Everyone likes dogs",
                Lessons = lessons
            };

            var lesson1ForSection2 = new LessonBO() {
                Title = "Lesson 2 title",
                VideoId = "long.mp4"
            };
            var otherlessons = new List<LessonBO>();
            otherlessons.Add(lesson1ForSection2);
            var section2 = new SectionBO() {
                Title = "Long video",
                Lessons = otherlessons
            };

            var lesson1ForSection3 = new LessonBO() {
                Title = "Lesson 1 title",
                VideoId = "dogs.mp4"
            };
            var section3Lessons = new List<LessonBO>();
            section3Lessons.Add(lesson1ForSection3);
            var section3 = new SectionBO() {
                Title = "Section 3",
                Lessons = section3Lessons
            };

            var sections = new List<SectionBO>();
            sections.Add(section);
            sections.Add(section2);
            sections.Add(section3);
            var course = new CourseBO() {
                Name = " Building Course",
                UserIds = userIds,
                CategoryId = favCategory.Id,
                Sections = sections
            };
            _courseService.Create(course);
            #endregion

            #region filler courses
            for (int i = 0; i < 50; i++) {
                var crs = new CourseBO() {
                    Name = " Course" + i,
                    UserIds = userIds,
                    CategoryId = favCategory.Id
                };
                if (i % 2 == 0) {
                    crs.UserIds = null;
                }
                _courseService.Create(crs);
            }
            #endregion
        }
    }
}
