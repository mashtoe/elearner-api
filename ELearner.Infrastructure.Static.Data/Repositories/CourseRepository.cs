using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Repositories {

    public class CourseRepository : ICourseRepository {

        readonly FakeDB _fakeDb;

        public CourseRepository(FakeDB fakeDB) {
            _fakeDb = fakeDB;
            if (_fakeDb.Students.Count < 1) {
                var student1 = new Student() {
                    Id = FakeDB.Id++,
                    Username = "student1"
                };
                _fakeDb.Students.Add(student1);
            }
        }

        public Course Create(Course course) {
            course.Id = FakeDB.Id++;

            if (course.Students != null) {
                // adding the reference between objects in the fake db
                foreach (var item in course.Students) {
                    item.CourseId = course.Id;
                    _fakeDb.StudentCourses.Add(item);
                }
            }
            course.Students = null;
            _fakeDb.Courses.Add(course);

            return course;
        }

        public Course Get(int id) {
            var course =  _fakeDb.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null) {
                course.Students = _fakeDb.StudentCourses.Where(sc => sc.CourseId == id).ToList();
            }
            return course;
        }

        public IEnumerable<Course> GetAll() {
            return _fakeDb.Courses;
        }

        public Course Update(Course course) {
            var entityFromDb = Get(course.Id);
            if (entityFromDb == null) return null;

            entityFromDb.Name = course.Name;
            return entityFromDb;
        }

        public Course Delete(int id) {
            var entityFromDb = Get(id);
            if (entityFromDb == null) return null;
            _fakeDb.Courses.Remove(entityFromDb);
            return entityFromDb;
        }

        public IEnumerable<Course> GetAllById(IEnumerable<int> ids) {
            var courses = _fakeDb.Courses.Where(c => ids.Contains(c.Id));
            return courses;
        }
    }
}
