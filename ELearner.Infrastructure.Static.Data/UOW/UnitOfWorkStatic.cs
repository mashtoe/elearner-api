using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using ELearner.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.UOW {
    class UnitOfWorkStatic : IUnitOfWork {

        public IStudentRepository StudentRepo { get; }
        public ICourseRepository CourseRepo { get; }

        public UnitOfWorkStatic() {
            StudentRepo = new StudentRepository();
            CourseRepo = new CourseRepository();
        }

        public int Complete() {
            return 0;
        }

        public void Dispose() {
        }
    }
}
