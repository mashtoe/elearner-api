using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class Course {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<UserCourse> Users { get; set; }
        public List<Section> Sections {get; set;}
    }
}
