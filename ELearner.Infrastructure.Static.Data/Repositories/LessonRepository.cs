using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Entities;

namespace ELearner.Infrastructure.Static.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        readonly FakeDB _fakeDB;
        public LessonRepository(FakeDB fakeDB)
        {
            _fakeDB = fakeDB;
        }
        public Lesson Create(Lesson lesson)
        {
            lesson.Id = FakeDB.Id++;

            _fakeDB.LessonsNotSaved.Add(lesson);
            return lesson;
        }
        public Lesson Get(int id)
        {
            var lesson = _fakeDB.LessonsNotSaved.FirstOrDefault(l => l.Id == id);
            return lesson;
        }
        public IEnumerable<Lesson> GetAll()
        {
            return _fakeDB.LessonsNotSaved;
        }
        public IEnumerable<Lesson> GetAllById(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        Lesson ILessonRepository.Delete(int id)
        {
            var lessonFromDb = Get(id);
            if (lessonFromDb == null) return null;

            _fakeDB.LessonsNotSaved.Remove(lessonFromDb);
            return lessonFromDb;
        }
    }
}