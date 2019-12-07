using System.Data.Entity;
using Common.Infrastructure;

namespace Aggregate.Persistence.StateInterface.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase<DataContext>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderStateMapping());
            modelBuilder.Configurations.Add(new OrderLineStateMapping());
        }
    }
}