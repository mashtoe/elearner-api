using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService.ServicesFacade {
    public interface IServicesFacade {
        IUserService UserService { get; }
        ICourseService CourseService { get; }
    }
}
