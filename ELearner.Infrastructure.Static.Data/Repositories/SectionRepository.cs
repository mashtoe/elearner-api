using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;

namespace ELearner.Infrastructure.Static.Data.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        readonly FakeDB _fakeDB;
        public SectionRepository(FakeDB fakeDB)
        {
            _fakeDB = fakeDB;
        }
        public Section Create(Section section)
        {
            section.Id = FakeDB.Id++;
          
            _fakeDB.SectionsNotSaved.Add(section);
            return section;
        }
        public Section Get(int id)
        {
            var section = _fakeDB.SectionsNotSaved.FirstOrDefault(s => s.Id == id);
            
            List<Lesson> lessons = null;
            if (section != null)
            {
                lessons = _fakeDB.LessonsNotSaved.Where(l => l.SectionId == id).ToList();
                section.Lessons = lessons;
            }
            return section;
        }

        public IEnumerable<Section> GetAll()
        {
            return _fakeDB.SectionsNotSaved;
        }
        public IEnumerable<Section> GetAllById(IEnumerable<int> ids)
        {
            var sections = _fakeDB.SectionsNotSaved.Where(s => ids.Contains(s.Id));
            return sections;
        }
        public Section Delete(int id)
        {
            var sectionFromDb = Get(id);
            if (sectionFromDb == null) return null;

            var referencesToRemove = _fakeDB.LessonsNotSaved.Where(l => l.SectionId == id).ToList();
            int count = referencesToRemove.Count();
            for (int i = 0; i < count; i++)
            {
                _fakeDB.LessonsNotSaved.Remove(referencesToRemove[i]);
            }
            
            _fakeDB.SectionsNotSaved.Remove(sectionFromDb);
            return sectionFromDb;
        }
    }
}