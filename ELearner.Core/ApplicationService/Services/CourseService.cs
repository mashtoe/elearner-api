using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {
    public class CourseService : ICourseService {
        readonly ICourseRepository _courseRepo;
        readonly CourseConverter _crsConv;

        readonly IStudentRepository _studRepo;
        readonly StudentConverter _studConv;


        public CourseService(ICourseRepository courseRepo, IStudentRepository studRepo) {
            _courseRepo = courseRepo;
            _studRepo = studRepo;
            _crsConv = new CourseConverter();
            _studConv = new StudentConverter();
        }
        public CourseBO New() {
            return new CourseBO();
        }

        public CourseBO Create(CourseBO course) {
            // TODO check if entity is valid, and throw errors if not
            var courseCreated = _courseRepo.Create(_crsConv.Convert(course));
            return _crsConv.Convert(courseCreated);
        }

        public CourseBO Delete(int id) {
            var courseDeleted = _courseRepo.Delete(id);
            return _crsConv.Convert(courseDeleted);
        }

        public CourseBO Get(int id) {
            var course = _crsConv.Convert(_courseRepo.Get(id));
            course.Students = _studRepo.GetAllById(course.StudentIds).Select(s => _studConv.Convert(s)).ToList();
            return course;
        }

        public List<CourseBO> GetAll() {
            var courses = _courseRepo.GetAll();
            return courses.Select(c => _crsConv.Convert(c)).ToList();
        }

        public CourseBO Update(CourseBO course) {
            var updatedCourse = _courseRepo.Update(_crsConv.Convert(course));
            return _crsConv.Convert(updatedCourse);
        }
    }
}
