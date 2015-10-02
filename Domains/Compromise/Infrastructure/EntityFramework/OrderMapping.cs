using System.Data.Entity.ModelConfiguration;
using Domains.Compromise.Domain;

namespace Domains.Compromise.Infrastructure.EntityFramework
{
    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public OrderMapping()
        {
            this.ToTable("Order");
            this.HasKey(x => x.Id);
            this.Property(x => x.OrderStatus);
            this.Property(x => x.TotalCost);
            this.Property(x => x.SubmitDate);
            this.HasMany(x => x.Lines).WithRequired().HasForeignKey(x=>x.OrderId);
        }
    }
}
