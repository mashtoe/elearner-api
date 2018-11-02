namespace ELearner.Core.Entity.Entities {
    public class UserCourse {

        public User User { get; set; }
        //[ForeignKey("StudentId")]
        public int UserID { get; set; }

        public Course Course { get; set; }
        //[ForeignKey("CourseId")]
        public int CourseId { get; set; }
    }
}
