using System.Data.Entity;
using Common.Infrastructure;

namespace Aggregate.Persistence.Compromise.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase<DataContext>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderMapping());
            modelBuilder.Configurations.Add(new OrderLineMapping());
        }
    }
}