using Elearner.Infrastructure.Data.Repositories;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elearner.Infrastructure.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IStudentRepository StudentRepo { get; }
        public ICourseRepository CourseRepo { get; }

        readonly ElearnerAppContext _context;

        public UnitOfWork(ElearnerAppContext context, IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _context = context;
            StudentRepo = new StudentRepository(context);
            CourseRepo = new CourseRepository(context);
        }

        /// <summary>
        /// </summary>
        /// <returns>Number of objects written to underlying database</returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        // When using UOW with a using statement, this method is called when thread exits using statement
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
