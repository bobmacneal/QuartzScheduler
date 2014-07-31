using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories
{
    public class OrderRequestRespository : DbContext, IOrderRequestRespository
    {
        public OrderRequestRespository()
            : base("name=OrderRequestRespository")
        {
        }

        public virtual DbSet<OrderRequest> OrderRequests { get; set; }

        public OrderRequest AddRequest(string payload)
        {
            var orderRequest = new OrderRequest
            {
                Status = 1,
                Payload = payload
            };
            OrderRequests.Add(orderRequest);
            SaveChanges();
            return orderRequest;
        }

        public IList<OrderRequest> GetUnproccessedOrderRequests()
        {
            IQueryable<OrderRequest> orderRequests =
                OrderRequests.Where(s => s.Status == (int) OrderStatusEnumeration.Initial);
            return orderRequests.ToList();
        }

        public void UpdateStatus(OrderRequest orderRequest, OrderStatusEnumeration orderProcessStatus)
        {
            orderRequest.Status = (int) orderProcessStatus;
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderRequest>()
                .Property(e => e.Payload)
                .IsUnicode(false);
        }

        public void DeleteRequest(OrderRequest orderRequest)
        {
            OrderRequests.Remove(orderRequest);
            SaveChanges();
        }
    }
}