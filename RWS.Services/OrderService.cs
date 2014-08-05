using System.Collections.Generic;
using Models;
using Models.Entities;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IErpOrderRespository _erpOrderRespository;
        private readonly IOrderRequestRespository _orderRequestRepository;

        public OrderService(IOrderRequestRespository orderRequestRepository, IErpOrderRespository erpOrderRespository)
        {
            _orderRequestRepository = orderRequestRepository;
            _erpOrderRespository = erpOrderRespository;
        }

        public OrderRequest AddOrderRequest(string requestPayload)
        {
            return _orderRequestRepository.AddRequest(requestPayload);
        }

        public IList<OrderRequest> GetUnproccessedOrderRequests()
        {
            return _orderRequestRepository.GetUnproccessedOrderRequests();
        }


        public void SetOrderStatusComplete(OrderRequest orderRequest)
        {
            _orderRequestRepository.UpdateStatus(orderRequest, OrderStatusEnumeration.Complete);
        }

        public void CreateErpOrder(OrderModel orderModel)
        {
            _erpOrderRespository.CreateOrder(orderModel);
        }

        public void DeleteOrderRequest(OrderRequest orderRequest)
        {
            _orderRequestRepository.DeleteRequest(orderRequest);
        }
    }
}