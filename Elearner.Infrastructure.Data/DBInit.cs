using ELearner.Core.Entity.Entities;

namespace Elearner.Infrastructure.Data
{
    public class DBInit
    {
        public static void SeedDB(ElearnerAppContext context) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var stud1 = context.Users.Add(new User()
            {
                Username = "BoringMan"
            }).Entity;

            context.Users.Add(new User()
            {
                Username = "FunnyMan"
            });
            context.Courses.Add(new Course() {
                   Name = "EFCourse"
            });
            context.SaveChanges();
        }
    }
}