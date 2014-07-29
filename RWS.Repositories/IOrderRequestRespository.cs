using System.Collections.Generic;
using System.Data.Entity;

namespace RWS.Repositories
{
    public interface IOrderRequestRespository
    {
        DbSet<OrderRequest> OrderRequests { get; set; }
        OrderRequest AddRequest(string payload);
        void DeleteRequest(OrderRequest orderRequest);
        IList<OrderRequest> GetUnproccessedOrderRequests();
        void UpdateStatus(OrderRequest orderRequest, OrderProcessStatusEnum orderProcessStatus);
    }
}