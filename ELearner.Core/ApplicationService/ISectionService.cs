using System.Collections.Generic;
using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.ApplicationService
{
    public interface ISectionService
    {
        SectionBO Create(SectionBO section);
        SectionBO Get(int id);
        //returns all sections (CRUD: Read)
        List<SectionBO> GetAll();
        //Update  (CRUD: Update)
        SectionBO Update(SectionBO section);
        //Delete one section with id (CRUD: Delete)
        SectionBO Delete(int id);
    }
}