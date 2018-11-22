namespace ELearner.Core.Entity.BusinessObjects
{
    public class SectionBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId {get; set;}
        public CourseBO Course {get; set; }
    }
}