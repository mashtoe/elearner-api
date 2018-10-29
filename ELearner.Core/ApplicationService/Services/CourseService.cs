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

        public CourseService(ICourseRepository courseRepo) {
            _courseRepo = courseRepo;
            _crsConv = new CourseConverter();
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
            var course = _courseRepo.Get(id);
            return _crsConv.Convert(course);
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
