using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService
{
    public interface IStudentService
    {
        //returns new student
        StudentBO New();
        //save one student (CRUD: Create)
        StudentBO Create(StudentBO student);
        //get one student by id (CRUD: Read)
        StudentBO Get(int id);
        //returns all students (CRUD: Read)
        List<StudentBO> GetAll();
        //Update  (CRUD: Update)
        StudentBO Update(StudentBO student);
        //Delete one student with id (CRUD: Delete)
        StudentBO Delete(int id);

    }
}
