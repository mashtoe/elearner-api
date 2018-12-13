using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class UndistributedCourseMaterial {
        public int Id { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }

        public string VideoId { get; set; }
    }
}
