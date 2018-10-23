using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService {
    public interface IStudentRepository {
        //Create Data
        //No Id on enter, but Id on exit
        Student Create(Student entity);
        //Read Data
        Student Get(int id);
        IEnumerable<Student> GetAll();
        //Update Data
        Student Update(Student entity);
        //Delete Data
        Student Delete(int id);
    }
}
