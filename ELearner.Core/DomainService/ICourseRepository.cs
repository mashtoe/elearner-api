using ELearner.Core.Entity;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;
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
        // (List<IFilterStrategy> filters = null) means filters can be null
        IEnumerable<Course> GetAll(List<IFilterStrategy> filters = null);
        // int Count(List<IFilterStrategy> filters = null);
        //Update Data
        Course Delete(int id);
        IEnumerable<Course> GetAllById(IEnumerable<int> ids);
    }
}
