using System;
using System.Linq;
using Patterns.Compromise.Domain;
using Patterns.Compromise.Infrastructure.EntityFramework;

namespace Patterns.Compromise.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                return dataContext
                    .Set<Order>()
                    .Include("Lines")
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(Order order)
        {
            using (var dataContext = new DataContext()) {
                dataContext.Set<Order>().Add(order);
                dataContext.SaveChanges();
            }
        }
    }
}