﻿using ELearner.Core.DomainService;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
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
                    _fakeDb.UserCourses.Add(item);
                }
            }
            course.Users = null;
            _fakeDb.Courses.Add(course);

            return course;
        }

        public Course Get(int id) {
            var course =  _fakeDb.Courses.FirstOrDefault(c => c.Id == id);
            List<UserCourse> users = null;
            if (course != null) {
                users = _fakeDb.UserCourses.Where(sc => sc.CourseId == id).ToList();
            }
            //return new object to avoid messing with the objects in the fake db
            return new Course() {
                Name = course.Name,
                Users = users
            };
        }

        public IEnumerable<Course> GetAll(Filter filter) {
            if (filter == null) {
                return _fakeDb.Courses;
            }
            var courses = _fakeDb.Courses
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize);
            return courses;
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
