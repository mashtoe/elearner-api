using System.Linq;
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
                Title = section.Title,
                CourseId = section.CourseId,
                Lessons = section.LessonIds?.Select(lId => new Lesson()
                {
                    SectionId = section.Id
                }).ToList()
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
                Title = section.Title,
                CourseId = section.CourseId,
                LessonIds = section.Lessons?.Select(l => l.Id).ToList(),

                //Course = new CourseConverter().Convert(section.Course)
            };
        }
    }
}