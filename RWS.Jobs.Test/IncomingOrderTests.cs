using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quartz;
using RWS.Models;
using RWS.Repositories;

namespace RWS.Jobs.UnitTests
{
    [TestClass]
    public class IncomingOrderTests
    {
        private IncomingOrderJob _job;
        private Mock<IJobExecutionContext> _jobExecutionContextMock;
        private Mock<IM3OrderRespository> _m3OrderRepositoryMock;
        private Mock<IOrderRequestRespository> _orderRequestRepositoryMock;
        private IList<OrderRequest> _unprocessedOrderRequests;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _jobExecutionContextMock = new Mock<IJobExecutionContext>();
            _orderRequestRepositoryMock = new Mock<IOrderRequestRespository>();
            _m3OrderRepositoryMock = new Mock<IM3OrderRespository>();
            _job = new IncomingOrderJob(_orderRequestRepositoryMock.Object, _m3OrderRepositoryMock.Object);

            _unprocessedOrderRequests = new List<OrderRequest>
            {
                new OrderRequest {Status = (int) OrderProcessStatusEnum.Initial, Payload = "test payload1"},
                new OrderRequest {Status = (int) OrderProcessStatusEnum.Initial, Payload = "test payload2"},
                new OrderRequest {Status = (int) OrderProcessStatusEnum.Initial, Payload = "test payload3"}
            };
            _orderRequestRepositoryMock.Setup(x => x.GetUnproccessedOrderRequests()).Returns(_unprocessedOrderRequests);
            _m3OrderRepositoryMock.Setup(x => x.CreateOrder(It.IsAny<OrderModel>()));
        }

        [TestMethod]
        public void Execute_ReadRecordsFromOrderRequestRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);

            _orderRequestRepositoryMock.Verify(x => x.GetUnproccessedOrderRequests());
        }

        [TestMethod]
        public void Execute_CollaboratesWithM3OrderRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);

            _m3OrderRepositoryMock.Verify(x => x.CreateOrder(It.IsAny<OrderModel>()), Times.Exactly(3));
        }

        [TestMethod]
        public void Execute_UpdatesStatusForRecordsFromOrderRequestRepository()
        {
            _job.Execute(_jobExecutionContextMock.Object);

            _orderRequestRepositoryMock.Verify(x => x.UpdateStatus(It.IsAny<OrderRequest>(), OrderProcessStatusEnum.Complete), Times.Exactly(3));
        }
    }
}