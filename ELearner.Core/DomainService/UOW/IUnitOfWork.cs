﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService.UOW
{
    public interface IUnitOfWork: IDisposable
    {
        // access to all repos, like a facade
        IUserRepository UserRepo { get; }
        ICourseRepository CourseRepo { get; }
        ISectionRepository SectionRepo{get;}
        ILessonRepository LessonRepo {get;}
        ICategoryRepository CategoryRepo {get;}
        IApplicationRepository ApplicationRepo {get;}
        int Complete();
    }
}
