using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
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
        public IEnumerable<Course> GetAll(Filter filter) {
            if (filter == null) {
                return _context.Courses;
            }
            return _context.Courses
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }
        //Update Data
        public Course Update(Course course) {
            var courseFromDb = Get(course.Id);
            courseFromDb.Name = course.Name;
            return courseFromDb;
        }
        //Delete Data
        public Course Delete(int id) {
            var courseFromDb = Get(id);
            _context.Remove(courseFromDb);
            return courseFromDb;
        }

        public IEnumerable<Course> GetAllById(IEnumerable<int> ids) {
            var courses = _context.Courses.Where(c => ids.Contains(c.Id));
            return courses;
        }
    }
}
