using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {
    public class CourseService : ICourseService {

        /*readonly ICourseRepository _courseRepo;
        readonly IStudentRepository _studRepo;*/

        readonly CourseConverter _crsConv;
        readonly StudentConverter _studConv;
        readonly IUnitOfWork _uow;


        public CourseService(IUnitOfWork uow) {
            _crsConv = new CourseConverter();
            _studConv = new StudentConverter();
            _uow = uow;
        }
        public CourseBO New() {
            return new CourseBO();
        }

        public CourseBO Create(CourseBO course) {
            using (_uow)
            {
                // TODO check if entity is valid, and throw errors if not
                var courseCreated = _uow.CourseRepo.Create(_crsConv.Convert(course));
                _uow.Complete();
                return _crsConv.Convert(courseCreated);
            }
                
        }

        public CourseBO Delete(int id) {
            using (_uow)
            {
                var courseDeleted = _uow.CourseRepo.Delete(id);
                _uow.Complete();
                return _crsConv.Convert(courseDeleted);
            }
        }

        public CourseBO Get(int id) {
            using (_uow)
            {
                var course = _crsConv.Convert(_uow.CourseRepo.Get(id));
                course.Students = _uow.StudentRepo.GetAllById(course.StudentIds).Select(s => _studConv.Convert(s)).ToList();
                return course;
            }
        }

        public List<CourseBO> GetAll() {
            using (_uow)
            {
                var courses = _uow.CourseRepo.GetAll();
                return courses.Select(c => _crsConv.Convert(c)).ToList();
            }
        }

        public CourseBO Update(CourseBO course) {
            using (_uow)
            {
                var updatedCourse = _uow.CourseRepo.Update(_crsConv.Convert(course));
                _uow.Complete();
                return _crsConv.Convert(updatedCourse);
            }
        }
    }
}
