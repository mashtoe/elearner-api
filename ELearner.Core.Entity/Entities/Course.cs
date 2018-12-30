using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class Course {

        #region omitted properties
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        // we want to save the progress that a user has made to a course, so he doesnt have to create
        // it and publish it in the same session
        // this bool decides if the course will be shown to all users or just the creator
        public bool Published { get; set; }

        public List<UserCourse> Users { get; set; }
        public List<Section> Sections { get; set; }
        // ? means it can be null
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Lesson> Lessons { get; set; }
        // public List<UndistributedCourseMaterial> UndistributedCourseMaterial { get; set; }
        #endregion

        public int? CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
