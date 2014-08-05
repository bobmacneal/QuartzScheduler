using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Moq;
using Quartz;
using Repositories;
using Services;

namespace Jobs.UnitTests
{
    [TestClass]
    public class IncomingOrderTests
    {
        private IncomingOrderJob _job;
        private Mock<IJobExecutionContext> _jobExecutionContextMock;
        private Mock<IOrderService> _orderServiceMock;
        private IList<OrderRequest> _unprocessedOrderRequests;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _jobExecutionContextMock = new Mock<IJobExecutionContext>();
            _job = new IncomingOrderJob(_orderServiceMock.Object);

            _unprocessedOrderRequests = new List<OrderRequest>
            {
                new OrderRequest {Status = (int) OrderStatusEnumeration.Initial, Payload = "test payload1"},
                new OrderRequest {Status = (int) OrderStatusEnumeration.Initial, Payload = "test payload2"},
                new OrderRequest {Status = (int) OrderStatusEnumeration.Initial, Payload = "test payload3"}
            };

            _orderServiceMock.Setup(x => x.GetUnproccessedOrderRequests()).Returns(_unprocessedOrderRequests);
            _orderServiceMock.Setup(x => x.CreateErpOrder(It.IsAny<OrderModel>()));
        }

        [TestMethod]
        public void Execute_ReadRecordsFromOrderRequestRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);
            _orderServiceMock.Verify(x => x.GetUnproccessedOrderRequests());
        }

        [TestMethod]
        public void Execute_CollaboratesWithM3OrderRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);
            _orderServiceMock.Verify(x => x.CreateErpOrder(It.IsAny<OrderModel>()), Times.Exactly(3));
        }

        [TestMethod]
        public void Execute_UpdatesStatusForRecordsFromOrderRequestRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);
            _orderServiceMock.Verify(
                x => x.SetOrderStatusComplete(It.IsAny<OrderRequest>()), Times.Exactly(3));
        }
    }
}