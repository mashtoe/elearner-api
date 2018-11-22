using System.Collections.Generic;

namespace ELearner.Core.Entity.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId {get; set; }
        public Course Course {get; set;}
        public List<Lesson> Lessons { get; set; }
    }
}