using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;

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
            var lessonFromDB = _context.Lessons.Add(lesson).Entity;
            return lessonFromDB;
        }

        public Lesson Get(int id)
        {
            return _context.Lessons
            .Include(l => l.Section)
            .FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons;
        }

        public IEnumerable<Lesson> GetAllById(IEnumerable<int> ids)
        {
            if (ids == null)
                return null;

            return _context.Lessons.Where(l => ids.Contains(l.Id));
        }


        public Lesson Delete(int id)
        {
            var lessonRemoved = Get(id);
            if (lessonRemoved == null) return null;
            _context.Remove(lessonRemoved);
            return lessonRemoved;
        }
    }
}