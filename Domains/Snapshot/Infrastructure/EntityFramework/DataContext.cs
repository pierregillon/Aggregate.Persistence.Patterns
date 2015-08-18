using System.Data.Entity;

namespace Domains.Snapshot.Infrastructure.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DomainModelPatterns.States")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderStateMapping());
            modelBuilder.Configurations.Add(new OrderLineStateMapping());
        }
    }
}
