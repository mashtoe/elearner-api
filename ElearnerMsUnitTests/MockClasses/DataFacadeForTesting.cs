using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElearnerMsUnitTests {
    public class DataFacadeForTesting : IDataFacade {
        public IUnitOfWork UnitOfWork { get; }
        /*get {
            return new UnitOfWorkMock();
        }*/
       
        public DataFacadeForTesting(IUnitOfWork uow) {
            UnitOfWork = uow;
        }
    }
}
