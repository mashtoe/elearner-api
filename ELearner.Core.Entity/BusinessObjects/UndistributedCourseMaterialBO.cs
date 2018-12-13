using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.BusinessObjects {
    public class UndistributedCourseMaterialBO {
        public int Id { get; set; }

        public CourseBO Course { get; set; }
        public int CourseId { get; set; }

        public string VideoId { get; set; }
    }
}
