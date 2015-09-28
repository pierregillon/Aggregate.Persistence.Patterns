using System.Data.Entity.ModelConfiguration;

namespace Domains.ModelInterface.Infrastructure.EntityFramework
{
    public class OrderStateMapping : EntityTypeConfiguration<PersistantOrder>
    {
        public OrderStateMapping()
        {
            this.ToTable("Order");
            this.HasKey(order => order.Id);
            this.Property(x => x.OrderStatus);
            this.Property(x => x.TotalCost);
            this.Property(x => x.SubmitDate).HasColumnType("datetime2");
            
            this.HasMany(x => x.Lines).WithRequired(x => x.Order);
        }
    }
}
