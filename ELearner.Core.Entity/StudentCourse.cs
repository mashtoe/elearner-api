using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity {

    // This is the binding table between Student and Course. 
    // We use this class to create a many-many relationship between them
    public class StudentCourse {

        public Student Student { get; set; }
        public int StudentId { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
