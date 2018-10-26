using Microsoft.EntityFrameworkCore;
using ELearner.Core.Entity;
using System.Collections.Generic;

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

            var studentCourse = new StudentCourse() {
                Course = new Course() {
                    Name = "AngularCourse",
                    Id = 1
                },
                Student = new Student() {
                    Username = "Bob",
                    Id = 1
                },
                CourseId = 1,
                StudentId = 1
            };
            var student = new Student() {
                Username = "Billy",
                Id = 2,
                Courses = new List<StudentCourse>() {
                     studentCourse
                }
            };
            modelBuilder.Entity<Student>()
                .HasData(student);
        }
    }
}