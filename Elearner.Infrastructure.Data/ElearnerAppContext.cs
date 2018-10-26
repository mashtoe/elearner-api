using Microsoft.EntityFrameworkCore;
using ELearner.Core.Entity;

namespace Elearner.Infrastructure.Data {
    public class ElearnerAppContext : DbContext {
        public ElearnerAppContext(DbContextOptions<ElearnerAppContext> options) : base(options) {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            #region Many-Many Student Course
            modelBuilder.Entity<StudentCourse>()
                .HasKey(t => new { t.StudentId, t.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(stud => stud.Courses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(crs => crs.Students)
                .HasForeignKey(sc => sc.StudentId);
            #endregion  
        }
    }
}