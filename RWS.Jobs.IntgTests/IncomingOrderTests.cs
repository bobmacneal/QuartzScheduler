using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Entities;
using Repositories;
using Services;

namespace Jobs.IntegrationTests
{
    [TestClass]
    public class IncomingOrderTests
    {
        private IncomingOrderJob _incomingOrderJob;
        private OrderRequest _request1;
        private OrderRequest _request2;
        private OrderRequest _request3;
        private IOrderService _orderService;


        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _orderService = new OrderService(new OrderRequestRespository(), new ErpOrderRespository());
            _incomingOrderJob = new IncomingOrderJob(_orderService);
            _request1 = _orderService.AddOrderRequest("test payload 1");
            _request2 = _orderService.AddOrderRequest("test payload 2");
            _request3 = _orderService.AddOrderRequest("test payload 3");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _orderService.DeleteOrderRequest(_request1);
            _orderService.DeleteOrderRequest(_request2);
            _orderService.DeleteOrderRequest(_request3);
        }

        [TestMethod]
        public void Execute()
        {
            IList<OrderRequest> unproccessedOrderRequestsBeforeProcessing =
                _orderService.GetUnproccessedOrderRequests();
            Assert.IsTrue(unproccessedOrderRequestsBeforeProcessing.Count >= 3);

            _incomingOrderJob.Execute(null);

            IList<OrderRequest> unproccessedOrderRequestsAfterProcessing =
                _orderService.GetUnproccessedOrderRequests();
            Assert.AreEqual(0, unproccessedOrderRequestsAfterProcessing.Count);
        }
    }
}