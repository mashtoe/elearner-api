using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Repositories {
    public class CourseRepository : ICourseRepository {
        public CourseRepository() {
            if (FakeDB.Students.Count < 1) {
                var student1 = new Student() {
                    Id = FakeDB.Id++,
                    Username = "student1"
                };
                FakeDB.Students.Add(student1);
            }
        }

        public Course Create(Course entity) {
            entity.Id = FakeDB.Id++;
            FakeDB.Courses.Add(entity);
            return entity;
        }

        public Course Get(int id) {
            return FakeDB.Courses.FirstOrDefault(course => course.Id == id);
        }

        public IEnumerable<Course> GetAll() {
            return FakeDB.Courses;
        }

        public Course Update(Course entity) {
            var entityFromDb = Get(entity.Id);
            if (entityFromDb == null) return null;

            entityFromDb.Name = entity.Name;
            return entityFromDb;
        }

        public Course Delete(int id) {
            var entityFromDb = Get(id);
            if (entityFromDb == null) return null;
            FakeDB.Courses.Remove(entityFromDb);
            return entityFromDb;
        }
    }
}
