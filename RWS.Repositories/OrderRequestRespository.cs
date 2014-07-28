using System;
using System.Data.Entity;

namespace RWS.Repositories
{
    public class OrderRequestRespository : DbContext
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderRequest>()
                .Property(e => e.Payload)
                .IsUnicode(false);
        }
    }
}