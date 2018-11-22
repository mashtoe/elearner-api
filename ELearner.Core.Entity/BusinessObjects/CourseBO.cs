using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.BusinessObjects {
    public class CourseBO {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserBO> Users { get; set; }

        public List<int> SectionIds {get; set; }
        public List<SectionBO> Sections {get; set;}
    }
}
