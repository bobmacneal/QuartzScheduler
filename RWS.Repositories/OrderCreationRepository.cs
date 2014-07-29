using System.Data.Entity;

namespace RWS.Repositories
{
    public class OrderCreationRepository : DbContext
    {
        public OrderCreationRepository()
            : base("name=OrderCreationRepository")
        {
        }

        public void CreateOrderMessage(Request request)
        {
            
        }

        public virtual DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
                .Property(e => e.SourceMessageID)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.CorrelationID)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.SourceDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.SourceMessage)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.CreatedByUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.LastUpdatedByUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.LastUpdatedByLogin)
                .IsUnicode(false);
        }
    }
}