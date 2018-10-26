using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService
{
    public interface IStudentService
    {
        //returns new student
        Student New();
        //save one student (CRUD: Create)
        Student Create(Student student);
        //get one student by id (CRUD: Read)
        Student Get(int id);
        //returns all students (CRUD: Read)
        List<Student> GetAll();
        //Update  (CRUD: Update)
        Student Update(Student student);
        //Delete one student with id (CRUD: Delete)
        Student Delete(int id);

    }
}
