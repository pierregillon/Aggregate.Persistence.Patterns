using System.Data.Entity;
using Domain.Base;
using Domain.Base.Infrastructure;

namespace Domains.EventSourcing.Infrastructure.EntityFramework
{
    public class DataContext : DataContextBase
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OrderEventMapping());
        }
    }
}