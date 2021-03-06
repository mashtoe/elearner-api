﻿using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data {
    public class FakeDB {

        private static FakeDB _instance;

        public static int Id = 1;

        public List<User> Users = new List<User>();
        public List<Course> Courses = new List<Course>();
        public List<UserCourse> UserCourses = new List<UserCourse>();
        public List<Section> Sections = new List<Section>();
        public List<Lesson> Lessons = new List<Lesson>();
        public List<Category> Categories = new List<Category>();

        public List<Application> Applications = new List<Application>();

        public List<User> UsersNotSaved = new List<User>();
        public List<Course> CoursesNotSaved = new List<Course>();
        public List<UserCourse> UserCoursesNotSaved = new List<UserCourse>();
        public List<Section> SectionsNotSaved = new List<Section>();
        public List<Lesson> LessonsNotSaved = new List<Lesson>();
        public List<Category> CategoriesNotSaved = new List<Category>();
        public List<Application> ApplicationsNotSaved = new List<Application>();

        private FakeDB() {

        }

        public static FakeDB GetInstance() {
            if (_instance == null) {
                _instance = new FakeDB();
            }
            return _instance;
        }
    }
}
