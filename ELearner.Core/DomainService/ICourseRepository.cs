using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService {
    public interface ICourseRepository {
        //Create Data
        //No Id on enter, but Id on exit
        Course Create(Course entity);
        //Read Data
        Course Get(int id);
        IEnumerable<Course> GetAll();
        //Update Data
        Course Update(Course entity);
        //Delete Data
        Course Delete(int id);
    }
}
