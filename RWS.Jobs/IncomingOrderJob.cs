using System;
using System.Collections.Generic;
using Models;
using Models.Entities;
using Quartz;
using Services;

namespace Jobs
{
    public class IncomingOrderJob : IJob
    {
        private readonly IOrderService _orderService;

        public IncomingOrderJob(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                IList<OrderRequest> unproccessedOrderRequests = _orderService.GetUnproccessedOrderRequests();
                foreach (OrderRequest unproccessedOrderRequest in unproccessedOrderRequests)
                {
                    var orderModel = new OrderModel();
                    _orderService.CreateErpOrder(orderModel);
                    _orderService.SetOrderStatusComplete(unproccessedOrderRequest);
                }
            }
            catch (Exception err)
            {
                throw new JobExecutionException(err);
            }
        }
    }
}