using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;

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
            return _context.Sections.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Section> GetAll()
        {
            return _context.Sections;
        }

        public IEnumerable<Section> GetAllById(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
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