using ELearner.Core.DomainService;
using ELearner.Core.Entity;
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
            _context.SaveChanges();
            return courseFromDb;
        }
        public Course Get(int id) {
            return _context.Courses.FirstOrDefault(course => course.Id == id);
        }
        public IEnumerable<Course> GetAll() {
            return _context.Courses;
        }
        //Update Data
        public Course Update(Course course) {
            var courseFromDb = Get(course.Id);
            courseFromDb.Name = course.Name;
            _context.SaveChanges();
            return courseFromDb;
        }
        //Delete Data
        public Course Delete(int id) {
            var courseFromDb = Get(id);
            _context.Remove(courseFromDb);
            _context.SaveChanges();
            return courseFromDb;
        }
    }
}
