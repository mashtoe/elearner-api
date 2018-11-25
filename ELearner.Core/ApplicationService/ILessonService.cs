using System.Collections.Generic;
using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.ApplicationService
{
    public interface ILessonService
    {
        LessonBO Create(LessonBO lesson);
        LessonBO Get(int id);
        //returns all lessons (CRUD: Read)
        List<LessonBO> GetAll();
        //Update  (CRUD: Update)
        LessonBO Update(LessonBO lesson);
        //Delete one lesson with id (CRUD: Delete)
        LessonBO Delete(int id);
    }
}