using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService {
    public interface ICourseService {
        //returns new course
        Course New();
        //save one course (CRUD: Create)
        Course Create(Course course);
        //get one course by id (CRUD: Read)
        Course Get(int id);
        //returns all courses (CRUD: Read)
        List<Course> GetAll();
        //Update  (CRUD: Update)
        Course Update(Course course);
        //Delete one course with id (CRUD: Delete)
        Course Delete(int id);
    }
}
