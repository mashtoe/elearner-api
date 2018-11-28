using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;

namespace ELearner.Infrastructure.Static.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly FakeDB _fakeDB;
        public ApplicationRepository(FakeDB fakeDB)
        {
            _fakeDB = fakeDB;
        }

        public Application Create(Application application)
        {
            application.Id = FakeDB.Id++;

            _fakeDB.ApplicationsNotSaved.Add(application);

            return application;
        }

        public Application Delete(int id)
        {
            var applicationFromDb = Get(id);
            if (applicationFromDb == null) return null;

            _fakeDB.ApplicationsNotSaved.Remove(applicationFromDb);
            return applicationFromDb;
        }

        public Application Get(int id)
        {
            var application = _fakeDB.ApplicationsNotSaved.FirstOrDefault(a => a.Id == id);

            return application;
        }

        public Application GetByUserId(int userId) {
            var application = _fakeDB.ApplicationsNotSaved.FirstOrDefault(a => a.UserId == userId);

            return application;
        }

        public IEnumerable<Application> GetAll()
        {
            return _fakeDB.ApplicationsNotSaved;
        }

        public IEnumerable<Application> GetAllById(IEnumerable<int> ids)
        {
            var applications = _fakeDB.ApplicationsNotSaved.Where(a => ids.Contains(a.Id));
            return applications;
        }

        
    }
}