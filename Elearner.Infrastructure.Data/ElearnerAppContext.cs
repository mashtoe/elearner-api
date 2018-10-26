using Microsoft.EntityFrameworkCore;
using ELearner.Core.Entity;

namespace Elearner.Infrastructure.Data {
    public class ElearnerAppContext : DbContext {
        public ElearnerAppContext(DbContextOptions<ElearnerAppContext> options) : base(options) {

        }

        public DbSet<Student> Students { get; set; }

    }
}