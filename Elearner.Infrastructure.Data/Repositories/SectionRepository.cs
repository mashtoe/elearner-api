using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        readonly ElearnerAppContext _context;
        public SectionRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public Section Create(Section section)
        {
            var sectionFromDB = _context.Sections.Add(section).Entity;
            return sectionFromDB;
        }
        public Section Get(int id)
        {
            return _context.Sections
            .Include(s => s.Course)
            .Include(s => s.Lessons)
            .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Section> GetAll()
        {
            return _context.Sections
            .Include(s => s.Course)
            .Include(s => s.Lessons)
            .ToList();
        }

        public IEnumerable<Section> GetAllById(IEnumerable<int> ids)
        {
            if(ids == null)
            return null;

            return _context.Sections.Where(s => ids.Contains(s.Id));
        }

        public Section Delete(int id)
        {
            var sectionRemoved = Get(id);
            if (sectionRemoved == null) return null;
            _context.Remove(sectionRemoved);
            return sectionRemoved;
        }
    }
}