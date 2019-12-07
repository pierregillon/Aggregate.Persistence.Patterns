using System.Data.Entity;
using Patterns.Contract.Infrastructure;

namespace Patterns.Compromise.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderMapping());
            modelBuilder.Configurations.Add(new OrderLineMapping());
        }
    }
}