namespace ELearner.Core.Entity.Entities {
    public class StudentCourse {

        public Student Student { get; set; }
        //[ForeignKey("StudentId")]
        public int StudentId { get; set; }

        public Course Course { get; set; }
        //[ForeignKey("CourseId")]
        public int CourseId { get; set; }
    }
}
