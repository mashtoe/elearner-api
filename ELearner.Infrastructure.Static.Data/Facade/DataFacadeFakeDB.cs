using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Infrastructure.Static.Data.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Facade {
    public class DataFacadeFakeDB : IDataFacade {
        public IUnitOfWork UnitOfWork {
            get {
                return new UnitOfWorkFakeDB();
            }
        }
    }
}
