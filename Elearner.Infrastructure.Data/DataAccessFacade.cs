using Elearner.Infrastructure.Data.UOW;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elearner.Infrastructure.Data {
    public class DataAccessFacade : IDataAccessFacade {
        readonly ElearnerAppContext _context;

        public IUnitOfWork UnitOfWork {
            get {
                return new UnitOfWork(_context);
            }
        }

        public DataAccessFacade(ElearnerAppContext context) {
            _context = context;
        }
    }
}
