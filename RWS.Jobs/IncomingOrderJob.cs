using System;
using System.Collections.Generic;
using Quartz;
using RWS.Models;
using RWS.Repositories;

namespace RWS.Jobs
{
    public class IncomingOrderJob : IJob
    {
        private readonly IM3OrderRespository _m3OrderRepository;
        private readonly IOrderRequestRespository _orderRequestRepository;

        public IncomingOrderJob(IOrderRequestRespository orderRequestRespository, IM3OrderRespository m3OrderRespository)
        {
            _m3OrderRepository = m3OrderRespository;
            _orderRequestRepository = orderRequestRespository;
        }

        //public IncomingOrderJob()
        //{
        //    _m3OrderRepository = new M3OrderRespository();
        //    _orderRequestRepository = new OrderRequestRespository();
        //}

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                IList<OrderRequest> unproccessedOrderRequests = _orderRequestRepository.GetUnproccessedOrderRequests();
                foreach (OrderRequest unproccessedOrderRequest in unproccessedOrderRequests)
                {
                    var orderModel = new OrderModel();
                    _m3OrderRepository.CreateOrder(orderModel);
                    _orderRequestRepository.UpdateStatus(unproccessedOrderRequest, OrderProcessStatusEnum.Complete);
                }
            }
            catch (Exception err)
            {
                throw new JobExecutionException(err);
            }
        }
    }
}