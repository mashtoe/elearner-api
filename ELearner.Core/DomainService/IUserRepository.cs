using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.DomainService {
    // the interfaces for our repositories are a part of the onions core, since we always can depend on the interface.
    // Since there might be multiple implementations of the interfaces & the implementations are subject to change, they are undependable 
    // and not a part of the core

    public interface IUserRepository {
        //Create Data
        //No Id on enter, but Id on exit
        User Create(User user);
        //Read Data
        // Should return user with all enrolled courses
        User Get(int id);
        IEnumerable<User> GetAll();
        //Delete Data
        User Delete(int id);
        IEnumerable<User> GetAllById(IEnumerable<int> ids);
    }
}
