using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.BusinessObjects {
    public class CourseBO {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<int> StudentIds { get; set; }
        public List<StudentBO> Students { get; set; }
    }
}
