using Elearner.Infrastructure.Data.Repositories;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Elearner.Infrastructure.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepo { get; }
        public ICourseRepository CourseRepo { get; }

        private ElearnerAppContext _context;
        //readonly IConfiguration _conf;

        public UnitOfWork(DbContextOptions<ElearnerAppContext> optionsBuilder)
        {
            /*var options = new DbContextOptionsBuilder<ElearnerAppContext>()
                .UseSqlite(conf.GetConnectionString("DefaultConnection"))
                    .Options;*/
            //_conf = conf;

            _context = new ElearnerAppContext(optionsBuilder);
            UserRepo = new UserRepository(_context);
            CourseRepo = new CourseRepository(_context);
        }

        /// <summary>
        /// </summary>
        /// <returns>Number of objects written to underlying database</returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        // When using UOW with a using statement, this method is called when thread exits using statement
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
