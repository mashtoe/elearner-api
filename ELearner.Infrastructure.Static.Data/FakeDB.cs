using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data {
    public static class FakeDB {
        public static int Id = 1;
        public static readonly List<Student> Students = new List<Student>();
        public static readonly List<Course> Courses = new List<Course>();

    }
}
