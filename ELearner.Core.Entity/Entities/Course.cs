using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class Course {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<UserCourse> Users { get; set; }
        public List<Section> Sections {get; set;}
        // ? means it can be null
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int CreaterId {get; set;}
        public User Creater {get; set;}

        public string Description {get; set;}
    }
}
