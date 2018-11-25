using ELearner.Core.Entity.Entities;
using System.Collections.Generic;

namespace ELearner.Core.Utilities.FilterStrategy {
    public interface IFilterStrategy {
        IEnumerable<Course> Filter(IEnumerable<Course> courses);
    }
}
