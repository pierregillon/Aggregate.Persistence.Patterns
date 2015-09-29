using System.Data.Entity;

namespace Domains.EventSourcing.Infrastructure.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DomainModelPatterns.EventSourcing") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderEventMapping());
        }
    }
}