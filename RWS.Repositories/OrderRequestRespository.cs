using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RWS.Repositories
{
    public class OrderRequestRespository : DbContext, IOrderRequestRespository
    {
        public OrderRequestRespository()
            : base("name=OrderRequestRespository")
        {
        }

        public virtual DbSet<OrderRequest> OrderRequests { get; set; }

        public bool AddRequest(string payload)
        {
            try
            {
                var orderRequest = new OrderRequest
                {
                    Status = 1,
                    Payload = payload
                };
                OrderRequests.Add(orderRequest);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<OrderRequest> GetUnproccessedOrderRequests()
        {
            using (var context = new OrderRequestRespository())
            {
                IQueryable<OrderRequest> orderRequests = context.OrderRequests.Where(s => s.Status == 1);
                return orderRequests.ToList();
            }
        }

        public void UpdateStatus(OrderRequest orderRequest, OrderProcessStatusEnum orderProcessStatus)
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
    }
}