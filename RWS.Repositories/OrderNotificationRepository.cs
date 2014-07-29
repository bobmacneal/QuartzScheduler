using System.Data.Entity;

namespace RWS.Repositories
{
    public class OrderNotificationRepository : DbContext
    {
        public OrderNotificationRepository()
            : base("name=OrderNotificationRepository")
        {
        }

        public virtual DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Response>()
                .Property(e => e.ResponseDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Response>()
                .Property(e => e.ResponseMessage)
                .IsUnicode(false);

            modelBuilder.Entity<Response>()
                .Property(e => e.CreatedByUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Response>()
                .Property(e => e.LastUpdatedByUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Response>()
                .Property(e => e.LastUpdatedByLogin)
                .IsUnicode(false);
        }
    }
}