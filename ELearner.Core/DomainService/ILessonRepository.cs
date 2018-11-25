using System.Collections.Generic;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.DomainService.UOW
{
    public interface ILessonRepository
    {
        //Create Data
        //No Id on Enter, but Id on exit
        Lesson Create(Lesson lesson);
        //Read Data
        Lesson Get(int id);
        IEnumerable<Lesson> GetAll();
        //Delete Data
        Lesson Delete(int id);
        IEnumerable<Lesson> GetAllById(IEnumerable<int> ids);
    }
}