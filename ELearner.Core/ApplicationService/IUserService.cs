using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService
{
    public interface IUserService
    {
        //get one user by id (CRUD: Read)
        UserBO Get(int id);
        //returns all users (CRUD: Read)
        List<UserBO> GetAll();
        //Update  (CRUD: Update)
        UserBO Update(UserBO user);
        //Delete one user with id (CRUD: Delete)
        UserBO Delete(int id);
        // Upgrades the users Role. (User => Educator) (Educator => Admin)
        UserBO Promote(int id);
        // Enables a course for a user
        UserBO Enroll(int courseId, int userId);
    }
}
