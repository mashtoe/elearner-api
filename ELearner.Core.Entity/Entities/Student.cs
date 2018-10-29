using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class Student {
        public string Username { get; set; }
        public int Id { get; set; }

        public List<StudentCourse> Courses { get; set; }
    }
}
