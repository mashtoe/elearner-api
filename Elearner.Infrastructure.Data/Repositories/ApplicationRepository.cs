using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ElearnerAppContext _context;
        public ApplicationRepository(ElearnerAppContext context)
        {
            _context = context;

        }
        public Application Create(Application application)
        {
            var applicationFromDB = _context.Applications.Add(application).Entity;
            return applicationFromDB;
        }
        public Application Get(int id)
        {
            return _context.Applications
            .Include(a => a.User)
            .FirstOrDefault(application => application.Id == id);
        }
        public IEnumerable<Application> GetAll()
        {
            return _context.Applications.Include(a => a.UserId);
        }
        public IEnumerable<Application> GetAllById(IEnumerable<int> ids)
        {
            if (ids == null)
                return null;

            return _context.Applications.Where(a => ids.Contains(a.Id));
        }
        public Application Delete(int id)
        {
            var applicationFromDb = Get(id);
            if (applicationFromDb == null) return null;
            _context.Remove(applicationFromDb);
            return applicationFromDb;
        }
    }
}