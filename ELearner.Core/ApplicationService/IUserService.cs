﻿using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.ApplicationService
{
    public interface IUserService
    {
        //returns new user
        UserBO New();
        //save one user (CRUD: Create)
        UserBO Create(UserBO user);
        //get one user by id (CRUD: Read)
        UserBO Get(int id);
        //returns all users (CRUD: Read)
        List<UserBO> GetAll();
        //Update  (CRUD: Update)
        UserBO Update(UserBO user);
        //Delete one user with id (CRUD: Delete)
        UserBO Delete(int id);
        // Upgrades the users UserRole. (User => Educator) (Educator => Admin)
        UserBO Promote(int id);

    }
}
