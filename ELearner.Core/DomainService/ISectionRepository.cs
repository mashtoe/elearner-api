
using System.Collections.Generic;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.DomainService
{
    public interface ISectionRepository
    {
        //Create Data
        //No Id on Enter, but Id on exit
        Section Create(Section section);
        //Read Data
         Section Get(int id);
        IEnumerable<Section> GetAll();
        //Delete Data
        Section Delete(int id);
        IEnumerable<Section> GetAllById(IEnumerable<int> ids);
    }
}