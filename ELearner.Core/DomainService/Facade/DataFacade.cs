using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService.Facade {
    public interface IDataFacade {
        IUnitOfWork UnitOfWork { get; }
    }
}
