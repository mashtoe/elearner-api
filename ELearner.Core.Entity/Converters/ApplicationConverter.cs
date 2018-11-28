using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Entity.Converters
{
    public class ApplicationConverter
    {
        public Application Convert(ApplicationBO application){
            if (application == null) {
                return null;
            }

            return new Application() {
                Id = application.Id,
                UserId = application.UserId
            };
        }

        public ApplicationBO Convert(Application application) {
            if (application == null)
            {
                return null;
            }

            return new ApplicationBO() {
                Id = application.Id,
                UserId = application.UserId
            };
        }
    }
}