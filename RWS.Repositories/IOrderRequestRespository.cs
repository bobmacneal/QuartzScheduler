using System.Collections.Generic;
using System.Data.Entity;

namespace RWS.Repositories
{
    public interface IOrderRequestRespository
    {
        DbSet<OrderRequest> OrderRequests { get; set; }
        bool AddRequest(string payload);
        IList<OrderRequest> GetUnproccessedOrderRequests();
        void UpdateStatus(OrderRequest orderRequest, OrderProcessStatusEnum orderProcessStatus);
    }
}