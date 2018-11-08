using System.Collections.Generic;
using System.Linq;
using ELearner.Core.Entity.Entities;

namespace ELearner.Core.Utilities.FilterStrategy {
    public class FilterOrderByNameStategy : IFilterStrategy {

        public string OrderBy { get; set; }

        public IEnumerable<Course> Filter(IEnumerable<Course> courses) {
            switch (OrderBy.ToLower()) {
                case "name": courses = courses.OrderBy(c => c.Name); break;
                case "id": courses = courses.OrderBy(c => c.Id); break;
            }
            return courses;
        }
    }
}
