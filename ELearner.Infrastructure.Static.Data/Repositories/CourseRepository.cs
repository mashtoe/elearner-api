using ELearner.Core.DomainService;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ELearner.Infrastructure.Static.Data.Repositories {

    public class CourseRepository : ICourseRepository {

        readonly FakeDB _fakeDb;

        public CourseRepository(FakeDB fakeDB) {
            _fakeDb = fakeDB;
        }

        public Course Create(Course course) {
            course.Id = FakeDB.Id++;

            if (course.Users != null) {
                // adding the reference between objects in the fake db
                foreach (var item in course.Users) {
                    item.CourseId = course.Id;
                    _fakeDb.UserCoursesNotSaved.Add(item);
                }
            }
            course.Users = null;
            _fakeDb.CoursesNotSaved.Add(course);

            return course;
        }

        public Course Get(int id) {
            var course =  _fakeDb.CoursesNotSaved.FirstOrDefault(c => c.Id == id);
            List<UserCourse> users = null;
            List<Section> sections = null;
            if (course != null) {
                users = _fakeDb.UserCoursesNotSaved.Where(sc => sc.CourseId == id).ToList();
                course.Users = users;
                sections = _fakeDb.SectionsNotSaved.Where(s => s.CourseId == id).ToList();
                course.Sections = sections;
            }
            //return new object to avoid messing with the objects in the fake db
            return course;
        }

        public IEnumerable<Course> GetAll(List<IFilterStrategy> filters) {
            IEnumerable<Course> courses = _fakeDb.CoursesNotSaved;
            if (filters != null) {
                for (int i = 0; i < filters.Count; i++) {
                    courses = filters[i].Filter(courses);
                }
            }
            return courses;
        }

        public Course Delete(int id) {
            var entityFromDb = Get(id);
            if (entityFromDb == null) return null;
            var referencesToRemove = _fakeDb.UserCoursesNotSaved.Where(uc => uc.CourseId == id).ToList();
            int count = referencesToRemove.Count();
            for (int i = 0; i < count; i++) {
                _fakeDb.UserCoursesNotSaved.Remove(referencesToRemove[i]);
            }
            var referencesTwoToRemove = _fakeDb.SectionsNotSaved.Where(s => s.CourseId == id).ToList();
            int countTwo = referencesTwoToRemove.Count();
            for (int i = 0; i < countTwo; i++)
            {
                _fakeDb.SectionsNotSaved.Remove(referencesTwoToRemove[i]);
            }
            _fakeDb.CoursesNotSaved.Remove(entityFromDb);
            return entityFromDb;
        }

        public IEnumerable<Course> GetAllById(IEnumerable<int> ids) {
            var courses = _fakeDb.CoursesNotSaved.Where(c => ids.Contains(c.Id));
            return courses;
        }
    }
}
