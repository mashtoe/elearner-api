namespace ELearner.Core.Entity.BusinessObjects
{
    public class CategoryBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public CourseBO Course { get; set; }
    }
}