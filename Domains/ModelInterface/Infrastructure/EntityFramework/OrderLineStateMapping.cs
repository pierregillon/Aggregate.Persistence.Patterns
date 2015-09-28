using System.Data.Entity.ModelConfiguration;

namespace Domains.ModelInterface.Infrastructure.EntityFramework
{
    public class OrderLineStateMapping : EntityTypeConfiguration<PersistantOrderLine>
    {
        public OrderLineStateMapping()
        {
            this.ToTable("OrderLine");
            this.HasKey(x => new {x.OrderId, x.Product});
            this.Property(x => x.OrderId);
            this.Property(x => x.Product);
            this.Property(x => x.Quantity);
        }
    }
}