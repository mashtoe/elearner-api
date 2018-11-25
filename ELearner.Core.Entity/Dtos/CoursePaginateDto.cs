using ELearner.Core.Entity.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Dtos {
    public class CoursePaginateDto {
        // 1 page of courses
        public List<CourseBO> Courses { get; set; }
        // total items
        public int Total { get; set; }
    }
}
