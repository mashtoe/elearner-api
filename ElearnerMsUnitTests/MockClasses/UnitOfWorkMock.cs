using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElearnerMsUnitTests.MockClasses {
    public class UnitOfWorkMock : IUnitOfWork {

        public IUserRepository UserRepo { get; }

        public ICourseRepository CourseRepo { get; }

        public ISectionRepository SectionRepo { get; }

        public ILessonRepository LessonRepo { get; }

        public ICategoryRepository CategoryRepo { get; }

        public IApplicationRepository ApplicationRepo { get; }

        public UnitOfWorkMock() {
            CourseRepo = new CourseRepositoryMock();
        }

        public int Complete() {
            return 0;
        }

        public void Dispose() {
        }
    }
}
