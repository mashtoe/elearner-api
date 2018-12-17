namespace ELearner.Core.Entity.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SectionId { get; set; }
        public Section Section { get; set; }
        public string VideoId { get; set; }

        // this is if this lesson is undistributed
        public Course Course { get; set; }
        public int? CourseId { get; set; }

        // index in current list (either section or undistributed lesson list)
        public int ListIndex { get; set; }
        // this is fix for the reload course on upload problem. used to decide which lesson should overwrite, if multiple have same id
        public bool IsNew { get; set; }

    }
}