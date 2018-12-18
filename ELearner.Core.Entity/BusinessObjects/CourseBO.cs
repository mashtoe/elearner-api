using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.BusinessObjects {
    public class CourseBO {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        // we want to save the progress that a user has made to a course, so he doesnt have to create
        // it and publish it in the same session
        // this bool decides if the course will be shown to all users or just the creator
        public bool Published { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserBO> Users { get; set; }
        public List<int> SectionIds {get; set; }
        public List<SectionBO> Sections {get; set;}
        // ? means it can be null
        public int? CategoryId { get; set; }
        public CategoryBO Category { get; set; }
        public int? CreatorId {get; set;}
        public UserBO Creator {get; set;}

        // public List<UndistributedCourseMaterialBO> UndistributedCourseMaterial { get; set; }
        public List<LessonBO> Lessons { get; set; }
    }
}
