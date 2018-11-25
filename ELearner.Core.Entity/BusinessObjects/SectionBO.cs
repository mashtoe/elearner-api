using System;
using System.Collections.Generic;
using System.Text;
namespace ELearner.Core.Entity.BusinessObjects
{
    public class SectionBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId {get; set;}
        public CourseBO Course {get; set; }
        public List<int> LessonIds { get; set; }
        public List<LessonBO> Lessons { get; set; }
    }
}