using System.Collections.Generic;
using Jobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;

namespace RWS.Jobs.IntgTests
{
    [TestClass]
    public class IncomingOrderTests
    {
        private IncomingOrderJob _incomingOrderJob;
        private IOrderRequestRespository _orderRequestRespository;
        private OrderRequest _request1;
        private OrderRequest _request2;
        private OrderRequest _request3;


        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _orderRequestRespository = new OrderRequestRespository();
            _incomingOrderJob = new IncomingOrderJob(_orderRequestRespository, new M3OrderRespository());
            _request1 = _orderRequestRespository.AddRequest("test payload 1");
            _request2 = _orderRequestRespository.AddRequest("test payload 2");
            _request3 = _orderRequestRespository.AddRequest("test payload 3");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _orderRequestRespository.DeleteRequest(_request1);
            _orderRequestRespository.DeleteRequest(_request2);
            _orderRequestRespository.DeleteRequest(_request3);
        }

        [TestMethod]
        public void Execute()
        {
            IList<OrderRequest> unproccessedOrderRequestsBeforeProcessing =
                _orderRequestRespository.GetUnproccessedOrderRequests();
            Assert.IsTrue(unproccessedOrderRequestsBeforeProcessing.Count >= 3);

            _incomingOrderJob.Execute(null);

            IList<OrderRequest> unproccessedOrderRequestsAfterProcessing =
                _orderRequestRespository.GetUnproccessedOrderRequests();
            Assert.AreEqual(0, unproccessedOrderRequestsAfterProcessing.Count);
        }
    }
}