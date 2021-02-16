using Elearner.Infrastructure.Data.UOW;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Elearner.Infrastructure.Data.Facade {
    public class DataFacade : IDataFacade {
        DbContextOptions<ElearnerAppContext> _options;
        public DataFacade(IConfiguration conf) {
            var options = new DbContextOptionsBuilder<ElearnerAppContext>()
                .UseSqlite(conf.GetConnectionString("DefaultConnection"))
                    .Options;
            _options = options;

            // use set RemoteFileHandler instead of LocalFileHandler (in startup) if you use UseSqlServer 
            // and do not include EnsureDeleted in ElearnerAppContext 

            /*
            var options = new DbContextOptionsBuilder<ElearnerAppContext>()
                .UseSqlServer(conf.GetConnectionString("Azure"))
                    .Options;
            _options = options;*/
        }
         
        public IUnitOfWork UnitOfWork {
            get {
                return new UnitOfWork(_options);
            }
        }
    }
}
