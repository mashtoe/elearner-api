namespace ELearner.Core.Entity.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}