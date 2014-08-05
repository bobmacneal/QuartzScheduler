using System.Collections.Generic;
using Models;
using Models.Entities;

namespace Services
{
    public interface IOrderService
    {
        OrderRequest AddOrderRequest(string requestPayload);
        void DeleteOrderRequest(OrderRequest ordeRequest);
        IList<OrderRequest> GetUnproccessedOrderRequests();
        void SetOrderStatusComplete(OrderRequest orderRequest);
        void CreateErpOrder(OrderModel orderModel);
    }
}