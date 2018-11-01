using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService.Facade {
    public interface IDataAccessFacade {
        IUnitOfWork UnitOfWork { get; }
    }
}
