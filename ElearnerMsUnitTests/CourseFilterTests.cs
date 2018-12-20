using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;
using ELearner.Core.Utilities.FilterStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElearnerMsUnitTests {

    [TestClass]
    public class CourseFilterTests {

        [DataRow(0,0)]
        [DataRow(19, 1)]
        [DataRow(0, 5)]
        [DataRow(-1, 5)]
        [DataRow(-123, 5)]
        [DataRow(0, 32323)]
        [DataTestMethod]
        public void TestPaginationFilterStrategyPageSize(int currentPage, int pageSize) {
            var filter = new FilterPaginateStrategy() { CurrentPage = currentPage, PageSize = pageSize };
            var unfilteredlList = CreateCourses(100);
            var filteredList = filter.Filter(unfilteredlList);
            var listCount = filteredList.Count();
            if (pageSize > listCount) {
                pageSize = listCount;
            }
            Assert.IsTrue(filteredList.Count() == pageSize);
        }

        [DataRow("1")]
        [DataTestMethod]
        public void TestSearchFilterStrategy(string query) {
            var filter = new FilterSearchStrategy() { Query = query};
            var unfilteredlList = new List<Course>();
            
            unfilteredlList.Add(new Course() { Name = "3hello", Description = "", Creator = new User() { Username = "" } });
            unfilteredlList.Add(new Course() { Name = "2hello", Description = "", Creator = new User() { Username = "" } });
            unfilteredlList.Add(new Course() { Name = "31ello", Description = "", Creator = new User() { Username = "" } });

            var filteredList = filter.Filter(unfilteredlList);

            bool expression = filteredList.Count() == 1 && filteredList.FirstOrDefault(c => c.Name.Equals("31ello")) != null;
            Assert.IsTrue(expression);
        }

        [DataRow("")]
        [DataTestMethod]
        public void TestSearchFilterStrategyNullValues(string query) {
            var filter = new FilterSearchStrategy() { Query = query };
            var unfilteredlList = new List<Course>();

            unfilteredlList.Add(new Course() { Name = null, Description = null, Creator = null });

            var filteredList = filter.Filter(unfilteredlList);

            bool expression = filteredList.Count() == 0;
            Assert.IsTrue(expression);
        }

        private List<Course> CreateCourses(int amount) {
            var returnList = new List<Course>();
            for (int i = 0; i < amount; i++) {
                returnList.Add(CreateCourseForTesting(i + 1, "" + i, ""));
            }
            return returnList;
        }

        private Course CreateCourseForTesting(int id, string name, string description) {
            var course = new Course() {
                Id = id,
                Name = name,
                Description = description,
                
                CategoryId = 1,
                Category = new Category() { Id = 1, Name = "Category1" },

                Creator = new User() { Id = 1, Role = ELearner.Core.Entity.Role.Educator, Username = "MockMan2" },
                CreatorId = 1
            };
            return course;
        }
    }
}
