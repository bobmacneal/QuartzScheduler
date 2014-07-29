using Microsoft.VisualStudio.TestTools.UnitTesting;
using RWS.Repositories;

namespace RWS.IntgTests.Repositories
{
    [TestClass]
    public class OrderRequestRepositoryTests
    {
        [TestMethod]
        public void AddRequest()
        {
            var repository = new OrderRequestRespository();
            Assert.IsTrue(repository.AddRequest("Test Payload"));
        }

        [TestMethod]
        public void GetUnproccessedOrderRequests()
        {
            var repository = new OrderRequestRespository();
            var orderRequests = repository.GetUnproccessedOrderRequests();
            if (orderRequests != null)
                Assert.IsTrue(orderRequests.Count >= 0);
        }
    }
}
