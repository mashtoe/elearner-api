using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService {
    // the interfaces for our repositories are a part of the onions core, since we always can depend on the interface.
    // Since there might be multiple implementations of the interfaces & the implementations are subject to change, they are undependable 
    // and not a part of the core

    public interface IStudentRepository {
        //Create Data
        //No Id on enter, but Id on exit
        Student Create(Student student);
        //Read Data
        // Should return student with all courses
        Student Get(int id);
        IEnumerable<Student> GetAll();
        //Update Data
        Student Update(Student student);
        //Delete Data
        Student Delete(int id);
        IEnumerable<Student> GetAllById(IEnumerable<int> ids);
    }
}
