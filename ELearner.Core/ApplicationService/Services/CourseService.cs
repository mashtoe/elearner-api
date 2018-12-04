using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;

namespace ELearner.Core.ApplicationService.Services
{
    public class CourseService : ICourseService
    {

        readonly CourseConverter _crsConv;
        readonly UserConverter _userConv;
        readonly SectionConverter _secConverter;
        readonly CategoryConverter _catConv;
        readonly LessonConverter _lesConv;
        readonly IDataFacade _facade;

        public CourseService(IDataFacade facade)
        {
            _crsConv = new CourseConverter();
            _userConv = new UserConverter();
            _secConverter = new SectionConverter();
            _catConv = new CategoryConverter();
            _lesConv = new LessonConverter();
            _facade = facade;
        }

        public CourseBO Create(CourseBO course)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var fullyConvertedCourse = ConvertCourseWithSectionsAndLessons(course);

                var courseCreated = uow.CourseRepo.Create(fullyConvertedCourse);
                uow.Complete();
                return _crsConv.Convert(courseCreated);
            }
        }

        public CourseBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var courseDeleted = uow.CourseRepo.Delete(id);
                if (courseDeleted == null)
                {
                    return null;
                }
                uow.Complete();
                return _crsConv.Convert(courseDeleted);
            }
        }

        public CourseBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork) {
                CourseBO course = null;
                var courseFromDb = uow.CourseRepo.Get(id);
                if (courseFromDb != null)
                {
                    var convCat = _catConv.Convert(courseFromDb.Category);
                    //var convSecs = courseFromDb.Sections?.Select(s => _secConverter.Convert(s)).ToList();
                    course = ConvertCourseWithSectionsAndLessons(courseFromDb);
                    course.Category = convCat;
                    //course.Sections = convSecs;
                    //course.Category = _catConv.Convert(uow.CategoryRepo.Get(course.CategoryId));
                    /*if (course.UserIds != null)
                    {
                        course.Users = uow.UserRepo.GetAllById(course.UserIds).Select(s => _userConv.Convert(s)).ToList();
                    }*/
                    /*if(course.SectionIds != null) {
                        course.Sections = uow.SectionRepo.GetAllById(course.SectionIds)
                        .Select(s => _secConverter.Convert(s)).ToList();
                    }*/
                }
                return course;
            }
        }

        public List<CourseBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                var courses = uow.CourseRepo.GetAll();
                return courses.Select(c => _crsConv.Convert(c)).ToList();
            }
        }
        public CoursePaginateDto GetFilteredOrders(Filter filter)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var filterStrats = new List<IFilterStrategy>();
                IFilterStrategy paginateStrat = null;
                if (filter != null)
                {
                    if (filter.FilterQueries != null)
                    {
                        foreach (var item in filter.FilterQueries)
                        {
                            filterStrats.Add(new FilterSearchStrategy() { Query = item });
                        }
                    }
                    if (filter.OrderBy != null)
                    {
                        filterStrats.Add(new FilterOrderByNameStategy() { OrderBy = filter.OrderBy });
                    }
                    if (filter.CurrentPage > -1 && filter.PageSize > 0)
                    {
                        paginateStrat = new FilterPaginateStrategy() { CurrentPage = filter.CurrentPage, PageSize = filter.PageSize };
                    }
                }
                IEnumerable<Course> courses;
                var count = 0;
                if (filterStrats.Count > 0 || paginateStrat != null)
                {
                    count = uow.CourseRepo.GetAll(filterStrats).Count();
                    if (paginateStrat != null) filterStrats.Add(paginateStrat);
                    courses = uow.CourseRepo.GetAll(filterStrats);
                }
                else
                {
                    // if there are no filters return all courses
                    count = uow.CourseRepo.GetAll().Count();
                    courses = uow.CourseRepo.GetAll();
                }
                var coursesConvereted = courses.Select(c => _crsConv.Convert(c)).ToList();

                return new CoursePaginateDto() { Total = count, Courses = coursesConvereted };
            }
        }

        public CourseBO Update(CourseBO course)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var courseFromDb = uow.CourseRepo.Get(course.Id);
                if (courseFromDb == null)
                {
                    return null;
                }
                courseFromDb.Name = course.Name;
                courseFromDb.CategoryId = course.CategoryId;
                uow.Complete();
                return _crsConv.Convert(courseFromDb);
            }
        }

        private Course ConvertCourseWithSectionsAndLessons(CourseBO course) {
            var listSectionsConverted = new List<Section>();
            if (course.Sections != null) {
                foreach (var section in course.Sections) {
                    //sections
                    if (section.Lessons != null) {
                        var listLessonsConverted = new List<Lesson>();
                        foreach (var lesson in section.Lessons) {
                            //lessons
                            var lessonConverted = _lesConv.Convert(lesson);
                            listLessonsConverted.Add(lessonConverted);
                        }
                        var convertedSection = _secConverter.Convert(section);
                        convertedSection.Lessons = listLessonsConverted;
                        listSectionsConverted.Add(convertedSection);
                    }
                }
            }
            var courseEntity = _crsConv.Convert(course);
            courseEntity.Sections = listSectionsConverted;
            return courseEntity;
        }

        private CourseBO ConvertCourseWithSectionsAndLessons(Course course) {
            var listSectionsConverted = new List<SectionBO>();
            if (course.Sections != null) {
                foreach (var section in course.Sections) {
                    //sections
                    if (section.Lessons != null) {
                        var listLessonsConverted = new List<LessonBO>();
                        foreach (var lesson in section.Lessons) {
                            //lessons
                            var lessonConverted = _lesConv.Convert(lesson);
                            listLessonsConverted.Add(lessonConverted);
                        }
                        var convertedSection = _secConverter.Convert(section);
                        convertedSection.Lessons = listLessonsConverted;
                        listSectionsConverted.Add(convertedSection);
                    }
                }
            }
            var courseEntity = _crsConv.Convert(course);
            courseEntity.Sections = listSectionsConverted;
            return courseEntity;
        }
    }
}
