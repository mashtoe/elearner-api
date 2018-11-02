using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data {
    public class FakeDB {

        private static FakeDB _instance;

        public static int Id = 1;
        public List<Student> Students = new List<Student>();
        public List<Course> Courses = new List<Course>();
        public List<StudentCourse> StudentCourses = new List<StudentCourse>();

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
