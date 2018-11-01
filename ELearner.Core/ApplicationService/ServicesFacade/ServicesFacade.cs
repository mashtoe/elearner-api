using ELearner.Core.ApplicationService.Services;
using ELearner.Core.DomainService.Facade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService.ServicesFacade {
    public class ServicesFacade : IServicesFacade {

        readonly IDataAccessFacade _dataAccessFacade;

        public IStudentService StudentService {
            get {
                return new StudentService(_dataAccessFacade);
            }
        }

        public ICourseService CourseService {
            get {
                return new CourseService(_dataAccessFacade);
            }
        }


        public ServicesFacade(IDataAccessFacade dataAccessFacade) {
            _dataAccessFacade = dataAccessFacade;
        }
    }
}
