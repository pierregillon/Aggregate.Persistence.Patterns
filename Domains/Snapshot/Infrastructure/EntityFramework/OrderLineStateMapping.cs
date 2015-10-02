using System.Data.Entity.ModelConfiguration;
using Domains.Snapshot.Domain;

namespace Domains.Snapshot.Infrastructure.EntityFramework
{
    public class OrderLineStateMapping : EntityTypeConfiguration<OrderLineState>
    {
        public OrderLineStateMapping()
        {
            this.ToTable("Snapshot_OrderLine");
            this.HasKey(x => new {x.OrderId, x.Product});
            this.Property(x => x.OrderId);
            this.Property(x => x.Product);
            this.Property(x => x.Quantity);
            this.Property(x => x.CreationDate).HasColumnType("datetime2");
        }
    }
}