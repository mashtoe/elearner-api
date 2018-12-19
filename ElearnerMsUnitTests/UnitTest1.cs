using ELearner.Core.Utilities.FilterStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElearnerMsUnitTests {
    [TestClass]
    public class UnitTest1 {



        [TestMethod]
        public void TestMethod1() {
            var filterByPaginateStrat = new FilterPaginateStrategy() { CurrentPage = 1, PageSize = 10};
            var x = 1;
            var y = 2;
            Assert.IsTrue(x + y == 2);
        }
    }
}
