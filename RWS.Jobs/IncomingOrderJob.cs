﻿using System;
using System.Collections.Generic;
using Models;
using Quartz;
using Repositories;

namespace Jobs
{
    public class IncomingOrderJob : IJob
    {
        private readonly IErpOrderRespository _m3OrderRepository;
        private readonly IOrderRequestRespository _orderRequestRepository;

        public IncomingOrderJob(IOrderRequestRespository orderRequestRespository, IErpOrderRespository m3OrderRespository)
        {
            _m3OrderRepository = m3OrderRespository;
            _orderRequestRepository = orderRequestRespository;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                IList<OrderRequest> unproccessedOrderRequests = _orderRequestRepository.GetUnproccessedOrderRequests();
                foreach (OrderRequest unproccessedOrderRequest in unproccessedOrderRequests)
                {
                    var orderModel = new OrderModel();
                    _m3OrderRepository.CreateOrder(orderModel);
                    _orderRequestRepository.UpdateStatus(unproccessedOrderRequest, OrderStatusEnumeration.Complete);
                }
            }
            catch (Exception err)
            {
                throw new JobExecutionException(err);
            }
        }
    }
}