//using Elearner.Core.DomainService;

using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly ElearnerAppContext _context;

        // cant use dependancy injection here since we neewd to call saveChanges on the context frsom the unitofwork
        // so we need to parse the context from the unitofwork
        public UserRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public User Create(User entity) {
            _context.Users.Add(entity);
            return entity;
        }

        public User Get(int id)
        {
            var user = _context.Users.Include(s => s.Courses).FirstOrDefault(u => u.Id == id);
            return user;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        //Update Data
        public User Update(User entity)
        {
            //var stud = _context.Students.Update(entity).Entity;
            var user = Get(entity.Id);
            user.Username = entity.Username;
            user.UserRole = entity.UserRole;
            return user;
        }
        //Delete Data
        public User Delete(int id)
        {
            var userRemoved = Get(id);
            _context.Remove(userRemoved);
            return userRemoved;
        }

        public IEnumerable<User> GetAllById(IEnumerable<int> ids) {
            var users = _context.Users.Where(e => ids.Contains(e.Id));
            return users;
        }
    }
}
