using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elearner.Infrastructure.Data.Repositories {
    public class CourseRepository: ICourseRepository {

        readonly ElearnerAppContext _context;

        public CourseRepository(ElearnerAppContext context) {
            _context = context;
        }
        public Course Create(Course course) {
            var courseFromDb = _context.Courses.Add(course).Entity;
            return courseFromDb;
        }
        public Course Get(int id) {
            return _context.Courses.Include(c => c.Users).FirstOrDefault(course => course.Id == id);
        }
        public IEnumerable<Course> GetAll(List<IFilterStrategy> filters) {
            if (filters == null) {
                return _context.Courses;
            }
            IEnumerable<Course> courses = _context.Courses;
            for (int i = 0; i < filters.Count; i++) {
                courses = filters[i].Filter(courses);
            }
            return courses;
        }
        
        //Delete Data
        public Course Delete(int id) {
            var courseFromDb = Get(id);
            if (courseFromDb == null) return null;
            _context.Remove(courseFromDb);
            return courseFromDb;
        }

        public IEnumerable<Course> GetAllById(IEnumerable<int> ids) {
            var courses = _context.Courses.Where(c => ids.Contains(c.Id));
            return courses;
        }
    }
}
