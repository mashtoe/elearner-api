using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;

namespace ELearner.Core.ApplicationService.Services {
    public class CourseService : ICourseService {

        readonly CourseConverter _crsConv;
        readonly UserConverter _userConv;
        readonly SectionConverter _secConverter;
        readonly CategoryConverter _catConv;
        readonly IDataFacade _facade;

        public CourseService(IDataFacade facade) {
            _crsConv = new CourseConverter();
            _userConv = new UserConverter();
            _secConverter = new SectionConverter();
            _catConv = new CategoryConverter();
            _facade = facade;
        }

        public CourseBO Create(CourseBO course) {
            using (var uow = _facade.UnitOfWork) {
                // TODO check if entity is valid, and throw errors if not
                var courseCreated = uow.CourseRepo.Create(_crsConv.Convert(course));
                uow.Complete();
                return _crsConv.Convert(courseCreated);
            }
        }

        public CourseBO Delete(int id) {
            using (var uow = _facade.UnitOfWork) {
                var courseToDelete = Get(id);
                if (courseToDelete == null) {
                    return null;
                }
                if (courseToDelete.SectionIds != null)
                {
                    foreach (var Id in courseToDelete?.SectionIds)
                    {
                        uow.SectionRepo.Delete(Id);
                    }
                }
                courseToDelete = _crsConv.Convert(uow.CourseRepo.Delete(id));
                uow.Complete();
                return courseToDelete;
            }
        }

        public CourseBO Get(int id) {
            using (var uow = _facade.UnitOfWork) {
                var course = _crsConv.Convert(uow.CourseRepo.Get(id));
                if (course != null) {
                    course.Category = _catConv.Convert(uow.CategoryRepo.Get(course.CategoryId));
                    if (course.UserIds != null) {
                        course.Users = uow.UserRepo.GetAllById(course.UserIds).Select(s => _userConv.Convert(s)).ToList();
                    }
                    if(course.SectionIds != null) {
                        course.Sections = uow.SectionRepo.GetAllById(course.SectionIds)
                        .Select(s => _secConverter.Convert(s)).ToList();
                    }
                }
                return course;
            }
        }

        public List<CourseBO> GetAll() {
            using (var uow = _facade.UnitOfWork) {
                var courses = uow.CourseRepo.GetAll();
                return courses.Select(c => _crsConv.Convert(c)).ToList();
            }
        }
        public CoursePaginateDto GetFilteredOrders(Filter filter) {
            using (var uow = _facade.UnitOfWork) {
                var filterStrats = new List<IFilterStrategy>();
                IFilterStrategy paginateStrat = null;
                if (filter != null) {
                    if (filter.FilterQueries != null) {
                        foreach (var item in filter.FilterQueries)
                        {
                            filterStrats.Add(new FilterSearchStrategy() { Query = item });
                        }
                    }
                    if (filter.OrderBy != null) {
                        filterStrats.Add(new FilterOrderByNameStategy() { OrderBy = filter.OrderBy });
                    }
                    if (filter.CurrentPage > -1 && filter.PageSize > 0) {
                        paginateStrat =  new FilterPaginateStrategy() { CurrentPage = filter.CurrentPage, PageSize = filter.PageSize };
                    }
                }
                IEnumerable<Course> courses;
                var count = 0;
                if (filterStrats.Count > 0 || paginateStrat != null) {
                    count = uow.CourseRepo.GetAll(filterStrats).Count();
                    if (paginateStrat != null) filterStrats.Add(paginateStrat);
                    courses = uow.CourseRepo.GetAll(filterStrats);
                } else {
                    // if there are no filters return all courses
                    count = uow.CourseRepo.GetAll().Count();
                    courses = uow.CourseRepo.GetAll();
                }
                var coursesConvereted = courses.Select(c => _crsConv.Convert(c)).ToList();

                return new CoursePaginateDto() { Total = count, Courses = coursesConvereted};
            }
        }

        public CourseBO Update(CourseBO course) {
            using (var uow = _facade.UnitOfWork) {
                var courseFromDb = uow.CourseRepo.Get(course.Id);
                if (courseFromDb == null) {
                    return null;
                }
                courseFromDb.Name = course.Name;
                courseFromDb.CategoryId = course.CategoryId;
                uow.Complete();
                return _crsConv.Convert(courseFromDb);
            }
        }
    }
}
