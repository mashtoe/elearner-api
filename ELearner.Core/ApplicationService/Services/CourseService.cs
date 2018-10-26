using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.Entity;

namespace ELearner.Core.ApplicationService.Services {
    public class CourseService : ICourseService {
        readonly ICourseRepository _courseRepo;

        public CourseService(ICourseRepository courseRepo) {
            _courseRepo = courseRepo;
        }
        public Course New() {
            return new Course();
        }

        public Course Create(Course course) {
            // TODO check if entity is valid, and throw errors if not
            return _courseRepo.Create(course);
        }

        public Course Delete(int id) {
            return _courseRepo.Delete(id);
        }

        public Course Get(int id) {
            return _courseRepo.Get(id);
        }

        public List<Course> GetAll() {
            return _courseRepo.GetAll().ToList();
        }

        public Course Update(Course course) {
            return _courseRepo.Update(course);
        }
    }
}
