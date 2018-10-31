using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        // access to all repos, like a facade
        IStudentRepository StudentRepo { get; }
        ICourseRepository CourseRepo { get; }
        int Complete();
    }
}
