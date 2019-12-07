using System.Data.Entity;
using Patterns.Contract.Infrastructure;

namespace Patterns.EventSourcing.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase<DataContext>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderEventMapping());
        }
    }
}