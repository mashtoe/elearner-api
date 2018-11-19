using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Entity.Converters
{
    public class LessonConverter
    {
        public Lesson Convert(LessonBO lesson)
        {
            if (lesson == null)
            {
                return null;
            }
            return new Lesson()
            {
                Id = lesson.Id,
                Title = lesson.Title
            };
        }

        public LessonBO Convert(Lesson lesson)
        {
            if (lesson == null)
            {
                return null;
            }
            return new LessonBO()
            {
                Id = lesson.Id,
                Title = lesson.Title
            };
        }
    }
}