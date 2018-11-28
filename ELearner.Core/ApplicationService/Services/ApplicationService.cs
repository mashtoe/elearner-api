using System.Collections.Generic;
using System.Linq;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services
{
    public class ApplicationService : IApplicationService
    {
        readonly ApplicationConverter _applicationConv;
        readonly UserConverter _userConv;
        private readonly IDataFacade _facade;
        public ApplicationService(IDataFacade facade)
        {
            _facade = facade;
            _applicationConv = new ApplicationConverter();
            _userConv = new UserConverter();
        }
        public ApplicationBO Create(ApplicationBO application)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var appliactionFromDb = uow.ApplicationRepo.GetByUserId(application.UserId);

                if (appliactionFromDb != null) {
                    // if user already created an appliaction he shouldn't be able to create another one
                    return null;
                }
                // TODO check if entity is valid, and throw errors if not
                var applicationCreated = uow.ApplicationRepo.Create(_applicationConv.Convert(application));
                uow.Complete();
                return _applicationConv.Convert(applicationCreated);
            }
        }
        public ApplicationBO Delete(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var applicationDeleted = uow.ApplicationRepo.Delete(id);
                if (applicationDeleted == null)
                {
                    return null;
                }
                uow.Complete();
                return _applicationConv.Convert(applicationDeleted);
            }
        }
        public ApplicationBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var applFromDb = uow.ApplicationRepo.Get(id);
                var convUser = _userConv.Convert(applFromDb.User);

                var application = _applicationConv.Convert(applFromDb);
                application.User = convUser;

                return application;
            }
        }
        public List<ApplicationBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                var returnList = new List<ApplicationBO>();

                var applicationsFromDB = uow.ApplicationRepo.GetAll();
                foreach (var entityFromDb in applicationsFromDB) {
                    var userConv = _userConv.Convert(entityFromDb.User);
                    var appConv = _applicationConv.Convert(entityFromDb);
                    appConv.User = userConv;
                    returnList.Add(appConv);
                }
                return returnList;
                // return applicationsFromDB.Select(a => _applicationConv.Convert(a)).ToList();
            }
        }
        
    }
}