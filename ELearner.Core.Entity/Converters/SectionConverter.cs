using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Entity.Converters
{
    public class SectionConverter
    {
        public Section Convert(SectionBO section) {
            if (section == null)
            {
                return null;
            }
            return new Section()
            {
                Id = section.Id,
                Title = section.Title
            };
        }

        public SectionBO Convert(Section section){
            if (section == null)
            {
                return null;
            }
            return new SectionBO()
            {
                Id = section.Id,
                Title = section.Title
            };
        }
    }
}