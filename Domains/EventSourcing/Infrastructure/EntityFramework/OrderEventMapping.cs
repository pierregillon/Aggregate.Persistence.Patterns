using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Domains.EventSourcing.Infrastructure.EntityFramework
{
    public class OrderEventMapping : EntityTypeConfiguration<OrderEvent>
    {
        public OrderEventMapping()
        {
            this.ToTable("OrderEvent");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.AggregateId);
            this.Property(x => x.CreationDate);
            this.Property(x => x.Name);
            this.Property(x => x.Content);
        }
    }
}