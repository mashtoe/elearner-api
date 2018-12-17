namespace ELearner.Core.Entity.BusinessObjects
{
    public class LessonBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SectionId { get; set; }
        public SectionBO Section { get; set; }
        public string VideoId { get; set; }

        // index in current list (either section or undistributed lesson list)
        public int ListIndex { get; set; }
    }
}