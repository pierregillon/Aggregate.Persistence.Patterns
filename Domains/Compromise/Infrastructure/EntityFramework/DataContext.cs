using System.Data.Entity;

namespace Domains.Compromise.Infrastructure.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DomainModelPatterns.States")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderMapping());
            modelBuilder.Configurations.Add(new OrderLineMapping());
        }
    }
}
