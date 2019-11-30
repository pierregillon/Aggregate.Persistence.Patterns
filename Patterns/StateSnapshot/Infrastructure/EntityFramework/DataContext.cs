using System.Data.Entity;
using Patterns.Contract.Infrastructure;

namespace Patterns.StateSnapshot.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderStateMapping());
            modelBuilder.Configurations.Add(new OrderLineStateMapping());
        }
    }
}