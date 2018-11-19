using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services
{
    public class LessonService : ILessonService
    {
        readonly LessonConverter _lessonConv;
        readonly IDataFacade _facade;
        public LessonService(IDataFacade facade)
        {
            _lessonConv = new LessonConverter();
            _facade = facade;
        }


        public LessonBO Create(LessonBO lesson)
        {
            using (var uow = _facade.UnitOfWork)
            {
                // TODO check if entity is valid, and throw errors if not
                var lessonCreated = uow.LessonRepo.Create(_lessonConv.Convert(lesson));
                uow.Complete();
                return _lessonConv.Convert(lessonCreated);
            }
        }
        public LessonBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var lesson = _lessonConv.Convert(uow.LessonRepo.Get(id));

                return lesson;
            }
        }

        public List<LessonBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                var lessons = uow.LessonRepo.GetAll();
                return lessons.Select(s => _lessonConv.Convert(s)).ToList();
            }
        }

        public LessonBO Update(LessonBO lesson)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var lessonFromDb = uow.LessonRepo.Get(lesson.Id);
                if (lessonFromDb == null)
                {
                    return null;
                }
                lessonFromDb.Title = lesson.Title;
                uow.Complete();
                return _lessonConv.Convert(lessonFromDb);
            }
        }

        public LessonBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var lessonDeleted = uow.LessonRepo.Delete(id);
                if (lessonDeleted == null)
                {
                    return null;
                }
                uow.Complete();
                return _lessonConv.Convert(lessonDeleted);
            }
        }
    }
}