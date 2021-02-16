using System;
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
                if (course == null) {
                    return null;
                }
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

                    var creatorConverted = _userConv.Convert(courseFromDb.Creator);
                    
                    //var convSecs = courseFromDb.Sections?.Select(s => _secConverter.Convert(s)).ToList();
                    course = ConvertCourseWithSectionsAndLessons(courseFromDb);
                    course.Category = convCat;
                    course.Creator = creatorConverted;

                    // we cant use orderby in query via entity framework since we get lessons via include
                    // so we gotta sort after the query is done
                    foreach (var section in course.Sections) {
                        
                        var sortedLessons = section.Lessons.OrderBy(l => l.ListIndex).ToList();
                        section.Lessons = sortedLessons;

                    }
                    var sortedUndistributedLessons = course.Lessons.OrderBy(l => l.ListIndex).ToList();
                    course.Lessons = sortedUndistributedLessons;
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
                // we only want to show the published courses to the user
                filterStrats.Add(new FilterByOnlyPublishedStrategy());
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
                    // nullable int
                    if (filter.UserId != null) {
                        var user = uow.UserRepo.Get((int)filter.UserId);

                        if (user != null) {
                            filterStrats.Add(new FilterEnrolledStrategy() { User = user });
                        }
                    }
                    if (filter.CategoryId != null) {
                        filterStrats.Add(new FilterByCategoryStrategy() { CategoryId = filter.CategoryId });
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

                var courselistObject = new List<CourseBO>();
                foreach (var course in courses)
                {
                    var creatorConverted = _userConv.Convert(course.Creator);
                    var courseConvereted = _crsConv.Convert(course);

                    courseConvereted.Creator = creatorConverted;
                    courselistObject.Add(courseConvereted);
                }
                return new CoursePaginateDto() { Total = count, Courses = courselistObject };
            }
        }

        public List<CourseBO> GetCreatorsCourses(int creatorId) {
            using (var uow = _facade.UnitOfWork) {
                var filterStrats = new List<IFilterStrategy>();
                filterStrats.Add(new FilterByCreatorStrategy() { CreatorId = creatorId });
                var courses = uow.CourseRepo.GetAll(filterStrats).OrderBy(c => c.Published);

                // conversion
                var courselistObject = new List<CourseBO>();
                foreach (var course in courses) {
                    var creatorConverted = _userConv.Convert(course.Creator);
                    var courseConvereted = _crsConv.Convert(course);

                    courseConvereted.Creator = creatorConverted;
                    courselistObject.Add(courseConvereted);
                }
                return courselistObject;
            }
        }

        public CourseBO Update(CourseBO course)
        {
            using (var uow = _facade.UnitOfWork)
            {
                List<SectionBO> sections = new List<SectionBO>(course.Sections);
                var courseFromDb = uow.CourseRepo.Get(course.Id);
                if (courseFromDb == null)
                {
                    return null;
                }
                var courseConverted = ConvertCourseWithSectionsAndLessons(course);

                courseFromDb.Name = courseConverted.Name;
                courseFromDb.CategoryId = courseConverted.CategoryId;
                courseFromDb.Description = courseConverted.Description;

                //1. Remove All, except the "old" ids we 
                //      wanna keep (Avoid attached issues)
                courseFromDb.Sections.RemoveAll(
                    sDb => !courseConverted.Sections.Exists(
                        s => s.Id == sDb.Id));

                //2. Remove All ids already in database 
                //      from customerUpdated
                courseConverted.Sections.RemoveAll(
                    s => courseFromDb.Sections.Exists(
                        sDb => s.Id == sDb.Id));

                //3. Add All new CustomerAddresses not 
                //      yet seen in the DB
                foreach (var item in courseConverted.Sections) {
                    item.Lessons.Clear();
                }
                courseFromDb.Sections.AddRange(courseConverted.Sections);
                // got error when -> upload new video -> create new section -> place new video in new section -> save changes
                // dirty solution
                uow.Complete();

                List<Section> sectionsConverted = ConvertSections(sections);
                for (int i = 0; i < courseFromDb.Sections.Count(); i++)// var sectionDirty in courseFromDb.Sections) {
                {
                    //1. 
                    courseFromDb.Sections[i].Lessons.RemoveAll(
                        lD => !sectionsConverted[i].Lessons.Exists(
                            l => l.Id == lD.Id));

                    //2. 
                    //remove all unmoved lessosn
                    sectionsConverted[i].Lessons.RemoveAll(
                        l => courseFromDb.Sections[i].Lessons.Exists(
                            l2 => l.Id == l2.Id));
                    //3.
                    //update all moved lessons section id individually
                    foreach (var item in sectionsConverted[i].Lessons) {
                        var lessonFromDb = uow.LessonRepo.Get(item.Id);
                        lessonFromDb.CourseId = null;
                        lessonFromDb.SectionId = courseFromDb.Sections[i].Id;
                    }
                }
                //1. Remove All, except the "old" ids we 
                //      wanna keep (Avoid attached issues)
                courseFromDb.Lessons.RemoveAll(
                    lDb => !courseConverted.Lessons.Exists(
                        l => l.Id == lDb.Id));

                //2. Remove All ids already in database 
                //      from customerUpdated
                courseConverted.Lessons.RemoveAll(
                    l => courseFromDb.Lessons.Exists(
                        lDb => l.Id == lDb.Id));

                //3. Add All new CustomerAddresses not 
                //      yet seen in the DB
                foreach (var item in courseConverted.Lessons) {
                    var lessonFromDb = uow.LessonRepo.Get(item.Id);
                    lessonFromDb.CourseId = null;
                    lessonFromDb.CourseId = courseFromDb.Id;
                }
                uow.Complete();
                // here the ListIndex of all lessons is set on the updated course
                var updatedCourseFromDb = uow.CourseRepo.Get(course.Id);
                for (int i = 0; i < updatedCourseFromDb.Sections.Count(); i++) {
                    var section = updatedCourseFromDb.Sections[i];
                    for (int j = 0; j < section.Lessons.Count(); j++) {
                        var lessonFromDb = section.Lessons[j];
                        var lesson = course.Sections[i].Lessons.FirstOrDefault(l => l.Id == lessonFromDb.Id);
                        lessonFromDb.ListIndex = lesson.ListIndex;
                    }
                }

                for (int i = 0; i < updatedCourseFromDb.Lessons.Count(); i++) {
                    var lessonFromDb = updatedCourseFromDb.Lessons[i];
                    var lesson = course.Lessons.FirstOrDefault(l => l.Id == lessonFromDb.Id);
                    lessonFromDb.ListIndex = lesson.ListIndex;
                }
                uow.Complete();
                return _crsConv.Convert(courseFromDb);
            }
        }

        public CourseBO Publish(int courseId, int idFromJwt) {
            using (var uow = _facade.UnitOfWork) {
                var courseFromDb = uow.CourseRepo.Get(courseId);
                if (courseFromDb == null) return null;
                if (courseFromDb.CreatorId != idFromJwt) {
                    return null;
                }
                if (courseFromDb.Published == false) {
                    courseFromDb.Published = true;
                } else {
                    return null;
                }
                uow.Complete();
                return _crsConv.Convert(courseFromDb);
            }
        }

        private List<Section> ConvertSections(List<SectionBO> sectionsToConvert) {
            var sectionsConverted = new List<Section>();
            foreach (var section in sectionsToConvert) {
                var lessonsConverted = new List<Lesson>();
                foreach (var lesson in section.Lessons) {
                    var lessonConverted = _lesConv.Convert(lesson);
                    lessonsConverted.Add(lessonConverted);
                }
                var sectionConverted = _secConverter.Convert(section);
                sectionConverted.Lessons = lessonsConverted;
                sectionsConverted.Add(sectionConverted);
            }
            return sectionsConverted;
        }

        private Course ConvertCourseWithSectionsAndLessons(CourseBO course) {

            var materialConverted = course.Lessons?.Select(l => _lesConv.Convert(l)).ToList();
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
            courseEntity.Lessons = materialConverted;
            return courseEntity;
        }

        private CourseBO ConvertCourseWithSectionsAndLessons(Course course) {
            var materialConverted = course.Lessons?.Select(l => _lesConv.Convert(l)).ToList();
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
            courseEntity.Lessons = materialConverted;
            return courseEntity;
        }


    }
}


/*
                    for (int j = 0; j < courseFromDb.Sections[i].Lessons.Count(); j++) {
                        if (courseFromDb.Sections[i].Lessons[j].IsNew) {
                            var duplicatedLesson = courseFromDb.Sections[i].Lessons[j];

                            var realLesson = courseFromDb.Lessons.FirstOrDefault(l => l.Id == courseFromDb.Sections[i].Lessons[j].Id);
                            realLesson.ListIndex = duplicatedLesson.ListIndex;
                            realLesson.CourseId = null;
                            realLesson.Course = null;
                            realLesson.SectionId = courseFromDb.Sections[i].Id;
                            courseFromDb.Lessons.Remove(realLesson);

                            //duplicatedLesson.SectionId = courseFromDb.Sections[i].Id;
                            duplicatedLesson.SectionId = null;
                            duplicatedLesson.CourseId = null;
                            duplicatedLesson.Course = null;
                            if (duplicatedLesson != null) {
                                courseFromDb.Sections[i].Lessons.Remove(duplicatedLesson);
                            }
                        }
                    }*/
