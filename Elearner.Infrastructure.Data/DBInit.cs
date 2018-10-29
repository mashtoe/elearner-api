using ELearner.Core.Entity.Entities;

namespace Elearner.Infrastructure.Data
{
    public class DBInit
    {
        public static void SeedDB(ElearnerAppContext context) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var stud1 = context.Students.Add(new Student()
            {
                Username = "BoringMan"
            }).Entity;

            context.Students.Add(new Student()
            {
                Username = "FunnyMan"
            });
            context.SaveChanges();
        }
    }
}