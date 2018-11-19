using System.Collections.Generic;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Entities;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        readonly ElearnerAppContext _context;
        public LessonRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public Lesson Create(Lesson lesson)
        {
            throw new System.NotImplementedException();
        }

        public Lesson Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Lesson Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Lesson> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Lesson> GetAllById(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}