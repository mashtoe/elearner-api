using ELearner.Core.Entity.BusinessObjects;

namespace ELearner.Core.Entity.Dtos {
    public class Filter {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        // public string FilterQuery { get; set; }
        public string[] FilterQueries { get; set; }
        public string OrderBy { get; set; }
        public int? UserId { get; set; }
    }
}
