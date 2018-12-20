using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElearnerMsUnitTests.MockClasses {
    public class CourseRepositoryMock : ICourseRepository {

        public Course Create(Course course) {
            return course;
        }

        public Course Delete(int id) {
            return new Course() {
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Random course" },
                Creator = new User() { Id = 1, Role = 0, Username = "MockMan" },
                Id = id,
                Name = "Mock Course",
                CreatorId = 1,
                Published = false,
                Description = "Mock course description"
            };
        }

        public Course Get(int id) {
            return GetMockCourse();
        }

        public IEnumerable<Course> GetAll(List<IFilterStrategy> filters = null) {
            var returnList = new List<Course>();
            returnList.Add(GetMockCourse());
            return returnList;
        }

        public IEnumerable<Course> GetAllById(IEnumerable<int> ids) {
            throw new NotImplementedException();
        }

        private Course GetMockCourse() {
            return new Course() {
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Random course" },
                Creator = new User() { Id = 1, Role = 0, Username = "MockMan" },
                Id = 1,
                Name = "Mock Course",
                CreatorId = 1,
                Published = false,
                Description = "Mock course description"
            };
        }
    }
}
