using ELearner.Core.ApplicationService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Utilities
{
    public class DataSeeder : IDataSeeder
    {

        readonly IAuthService _authService;
        readonly ICourseService _courseService;
        readonly IUserService _userService;
        readonly ISectionService _secService;
        readonly ICategoryService _catService;
        readonly ILessonService _lesService;


        public DataSeeder(IAuthService authService, ICourseService courseService, IUserService userService, ICategoryService categoryService, ISectionService sectionService, ILessonService lessonService)
        {
            _authService = authService;
            _courseService = courseService;
            _userService = userService;
            _catService = categoryService;
            _secService = sectionService;
            _lesService = lessonService;
        }

        public void SeedData()
        {
            #region User creation
            var user = new UserRegisterDto()
            {
                Username = "UserMan",
                Password = "secretpassword"
            };
            var userCreated = _authService.Register(user);

            var educator = new UserRegisterDto()
            {
                Username = "EducatorMan",
                Password = "secretpassword"
            };

            var educatorCreated = _authService.Register(educator);

            _userService.Promote(educatorCreated.Id);

            var admin = new UserRegisterDto()
            {
                Username = "AdminMan",
                Password = "secretpassword"
            };

            _userService.Promote(_userService.Promote(_userService.Promote(_authService.Register(admin).Id).Id).Id);
            List<int> userIds = new List<int>();
            userIds.Add(userCreated.Id);

            var category = new CategoryBO()
            {
                Name = "Math"
            };
            #endregion

            var favCategory = _catService.Create(category);

            #region Building course
            var lessons = new List<LessonBO>();
            for (int i = 0; i < 20; i++)
            {
                var lesson = new LessonBO()
                {
                    Title = "Hello" + i,
                    VideoId = "dogs.mp4"
                };
                lessons.Add(lesson);

            }
            var section = new SectionBO()
            {
                Title = "Everyone likes dogs",
                Lessons = lessons
            };

            var lesson1ForSection2 = new LessonBO()
            {
                Title = "Lesson 2 title",
                VideoId = "long.mp4"
            };
            var otherlessons = new List<LessonBO>();
            otherlessons.Add(lesson1ForSection2);
            var section2 = new SectionBO()
            {
                Title = "Long video",
                Lessons = otherlessons
            };

            var lesson1ForSection3 = new LessonBO()
            {
                Title = "Lesson 1 title",
                VideoId = "dogs.mp4"
            };
            var section3Lessons = new List<LessonBO>();
            section3Lessons.Add(lesson1ForSection3);
            var section3 = new SectionBO()
            {
                Title = "Section 3",
                Lessons = section3Lessons
            };

            var sections = new List<SectionBO>();
            sections.Add(section);
            sections.Add(section2);
            //sections.Add(section3);

            var material = new List<LessonBO>();
            material.Add(new LessonBO() {
                VideoId = "dogs.mp4",
                Title = "Dogs"
            });
            material.Add(new LessonBO() {
                VideoId = "long.mp4",
                Title = "Long"
            });


            var course = new CourseBO()
            {
                Name = " Building Course",
                UserIds = userIds,
                CategoryId = favCategory.Id,
                Sections = sections,
                CreatorId = educatorCreated.Id,
                Published = true,
                Description = "Your body can’t digest corn. So if you ate literally nothing but corn every day you’d reach the point where you’re shitting out pure corn and then you’ve got an infinite food source.",
                Lessons = material
            };
            _courseService.Create(course);
            #endregion

            #region filler courses
            for (int i = 0; i < 50; i++)
            {
                bool published = true;
                if (i % 10 == 0)
                {
                    published = false;
                    // crsUserIds.Add(userCreated.Id);
                }

                List<int> crsUserIds = new List<int>();
                if ((i + 1) % 2 == 0)
                {
                    crsUserIds.Add(userCreated.Id);
                }
                if ((i + 1) % 13 == 0)
                {
                    crsUserIds.Add(educatorCreated.Id);
                }
                string flower = "flower";
                if (i > 1 || i < 1) flower += "s";
                var crs = new CourseBO()
                {
                    Name = " Course" + i,

                    CreatorId = educatorCreated.Id,
                    UserIds = crsUserIds,
                    CategoryId = favCategory.Id,
                    Description = i + " " + flower + " in the garden",
                    Published = published
                };

                _courseService.Create(crs);
            }
            #endregion

            #region categories

            var cat_1 = new CategoryBO()
            {
                Name = "Electronics"
            };
            var cat_2 = new CategoryBO()
            {
                Name = "Business "
            };
            var cat_3 = new CategoryBO()
            {
                Name = "Robotics"
            };
            var cat_4 = new CategoryBO()
            {
                Name = "Graphic Design"
            };
            var cat_5 = new CategoryBO()
            {
                Name = "Information Technology"
            };
            var cat_6 = new CategoryBO()
            {
                Name = "English"
            };
            var cat_7 = new CategoryBO()
            {
                Name = "Spanish"
            };
            var cat_8 = new CategoryBO()
            {
                Name = "Visual Arts"
            };
             var cat_9 = new CategoryBO()
            {
                Name = "Marketing"
            };
             var cat_10 = new CategoryBO()
            {
                Name = "Productivity"
            };
             var cat_11 = new CategoryBO()
            {
                Name = "Leadership"
            };

            var cat1 = _catService.Create(cat_1);
            var cat2 = _catService.Create(cat_2);
            var cat3 = _catService.Create(cat_3);
            var cat4 = _catService.Create(cat_4);
            var cat5 = _catService.Create(cat_5);
            var cat6 = _catService.Create(cat_6);
            var cat7 = _catService.Create(cat_7);
            var cat8 = _catService.Create(cat_8);
            var cat9 = _catService.Create(cat_9);
            var cat10 = _catService.Create(cat_10);
            var cat11 = _catService.Create(cat_11);
    
            #endregion

        }
    }
}
