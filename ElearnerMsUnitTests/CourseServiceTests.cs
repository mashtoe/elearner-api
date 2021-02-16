using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.Services;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Utilities.FilterStrategy;
using ElearnerMsUnitTests.MockClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElearnerMsUnitTests {
    [TestClass]
    public class CourseServiceTests {

        readonly IDataFacade _facadeMock;
        readonly IUnitOfWork _uowMock;

        readonly ILessonRepository _lessonRepoMock;
        readonly ICourseRepository _courseRepoMock;

        public CourseServiceTests() {
            _uowMock = new UnitOfWorkMock();
            _facadeMock = new DataFacadeForTesting(_uowMock);

        }

        [TestMethod]
        public void TestCreateWithNull() {
            var service = CreateMockCourseService();
            var createdCourse = service.Create(null);
            Assert.IsNull(createdCourse);
        }

        [TestMethod]
        public void TestCreateMethodNameIsEqual() {
            var service = CreateMockCourseService();
            var courseBo = CreateCourseForTesting();
            var createdCourse = service.Create(courseBo);
            Assert.IsTrue(createdCourse.Name.Equals(courseBo.Name));
        }

        [TestMethod]
        public void TestDeleteCourse() {
            int id = 4;
            var service = CreateMockCourseService();
            var course = service.Delete(id);
            Assert.AreEqual(course.Id, id);
        }

        public ICourseService CreateMockCourseService() {
            return new CourseService(_facadeMock);
        }

        private CourseBO CreateCourseForTesting() {
            var courseBo = new CourseBO() {
                Id = 1,
                Name = "Course of creation",
                Description = "Description",

                CategoryId = 1,
                Category = new CategoryBO() { Id = 1, Name = "Category1" },

                Creator = new UserBO() { Id = 1, Role = ELearner.Core.Entity.Role.Educator, Username = "MockMan2" },
                CreatorId = 1
            };
            return courseBo;
        }
    }
}
