using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService {
    public interface ICourseService {
        //returns new course
        CourseBO New();
        //save one course (CRUD: Create)
        CourseBO Create(CourseBO course);
        //get one course by id (CRUD: Read)
        CourseBO Get(int id);
        //returns all courses (CRUD: Read)
        List<CourseBO> GetAll();
        //Update  (CRUD: Update)
        CourseBO Update(CourseBO course);
        //Delete one course with id (CRUD: Delete)
        CourseBO Delete(int id);
        List<CourseBO> GetFilteredOrders(Filter filter);
    }
}
