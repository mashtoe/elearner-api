using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Infrastructure.Static.Data.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data {
    public class DataAccessFacadeStatic : IDataAccessFacade {

        public IUnitOfWork UnitOfWork {
            get {
                return new UnitOfWorkStatic();
            }
        }
    }
}
