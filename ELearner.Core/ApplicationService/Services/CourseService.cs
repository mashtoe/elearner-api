﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Utilities.FilterStrategy;

namespace ELearner.Core.ApplicationService.Services {
    public class CourseService : ICourseService {

        readonly CourseConverter _crsConv;
        readonly UserConverter _userConv;
        //readonly IUnitOfWork _uow;
        readonly IDataFacade _facade;

        public CourseService(IDataFacade facade) {
            _crsConv = new CourseConverter();
            _userConv = new UserConverter();
            _facade = facade;
        }
        public CourseBO New() {
            return new CourseBO();
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
                var courseDeleted = uow.CourseRepo.Delete(id);
                uow.Complete();
                return _crsConv.Convert(courseDeleted);
            }
        }

        public CourseBO Get(int id) {
            using (var uow = _facade.UnitOfWork) {
                var course = _crsConv.Convert(uow.CourseRepo.Get(id));
                if (course != null) {
                    course.Users = uow.UserRepo.GetAllById(course.UserIds).Select(s => _userConv.Convert(s)).ToList();
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

        public List<CourseBO> GetFilteredOrders(Filter filter) {
            using (var uow = _facade.UnitOfWork) {
                var filterStrats = new List<IFilterStrategy>();
                if (filter != null) {

                    if (filter.FilterQuery != null) {
                        filterStrats.Add(new FilterSearchStrategy() { Query = filter.FilterQuery });
                    }
                    if (filter.OrderBy != null) {
                        filterStrats.Add(new FilterOrderByNameStategy() { OrderBy = filter.OrderBy });
                    }
                    if (filter.CurrentPage > -1 && filter.PageSize > 0) {
                        filterStrats.Add(new FilterPaginateStrategy() { CurrentPage = filter.CurrentPage, PageSize = filter.PageSize });
                    }
                }
                var courses = uow.CourseRepo.GetAll(filterStrats);
                return courses.Select(c => _crsConv.Convert(c)).ToList();
            }
        }

        public CourseBO Update(CourseBO course) {
            using (var uow = _facade.UnitOfWork) {
                var updatedCourse = uow.CourseRepo.Update(_crsConv.Convert(course));
                uow.Complete();
                return _crsConv.Convert(updatedCourse);
            }
        }

        
    }
}
