using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService.ServicesFacade {
    public interface IServicesFacade {
        IStudentService StudentService { get; }
        ICourseService CourseService { get; }
    }
}
