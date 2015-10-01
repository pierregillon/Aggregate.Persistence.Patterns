using System.Data.Entity.ModelConfiguration;
using Domains.Snapshot.Domain;

namespace Domains.Snapshot.Infrastructure.EntityFramework
{
    public class OrderStateMapping : EntityTypeConfiguration<OrderState>
    {
        public OrderStateMapping()
        {
            this.ToTable("Order");
            this.HasKey(x => x.Id);
            this.Property(x => x.OrderStatus);
            this.Property(x => x.TotalCost);
            this.Property(x => x.SubmitDate).HasColumnType("datetime2");
            this.HasMany(x => x.Lines).WithRequired(x => x.Order);
        }
    }
}
