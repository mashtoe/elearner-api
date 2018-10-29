using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService {
    public interface ICourseRepository {
        //Create Data
        //No Id on enter, but Id on exit
        Course Create(Course course);
        //Read Data
        Course Get(int id);
        IEnumerable<Course> GetAll();
        //Update Data
        Course Update(Course course);
        //Delete Data
        Course Delete(int id);
    }
}
