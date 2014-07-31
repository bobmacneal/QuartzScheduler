using System.Collections.Generic;
using System.Data.Entity;

namespace Repositories
{
    public interface IOrderRequestRespository
    {
        OrderRequest AddRequest(string payload);
        void DeleteRequest(OrderRequest orderRequest);
        IList<OrderRequest> GetUnproccessedOrderRequests();
        DbSet<OrderRequest> OrderRequests { get; set; }
        void UpdateStatus(OrderRequest orderRequest, OrderStatusEnumeration orderStatus);
    }
}