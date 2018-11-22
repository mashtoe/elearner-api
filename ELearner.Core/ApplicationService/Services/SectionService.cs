using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services
{
    public class SectionService : ISectionService
    {
        readonly SectionConverter _sectionConv;
        readonly CourseConverter _crsConv;
        readonly IDataFacade _facade;
        public SectionService(IDataFacade facade)
        {
            _sectionConv = new SectionConverter();
            _crsConv = new CourseConverter();
            _facade = facade;
        }

        public SectionBO Create(SectionBO section)
        {
            using (var uow = _facade.UnitOfWork)
            {
                // TODO check if entity is valid, and throw errors if not
                var sectionCreated = uow.SectionRepo.Create(_sectionConv.Convert(section));
                uow.Complete();
                return _sectionConv.Convert(sectionCreated);
            }
        }
        public SectionBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var section = _sectionConv.Convert(uow.SectionRepo.Get(id));
                if (section != null)
                {
                    section.Course = _crsConv.Convert(uow.CourseRepo.Get(section.CourseId));
                }
                uow.Complete();
               
                return section;
            }
        }
        public List<SectionBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                var sections = uow.SectionRepo.GetAll();
                return sections.Select(s => _sectionConv.Convert(s)).ToList();
            }
        }
        public SectionBO Update(SectionBO section)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var sectionFromDb = uow.SectionRepo.Get(section.Id);
                if (sectionFromDb == null)
                {
                    return null;
                }

                sectionFromDb.Title = section.Title;
                sectionFromDb.CourseId = section.CourseId;
                uow.Complete();
                return _sectionConv.Convert(sectionFromDb);
            }
        }
        public SectionBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var sectionDeleted = uow.SectionRepo.Delete(id);
                if (sectionDeleted == null)
                {
                    return null;
                }
                uow.Complete();
                return _sectionConv.Convert(sectionDeleted);
            }
        }
    }
}