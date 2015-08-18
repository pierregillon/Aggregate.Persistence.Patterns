using System;
using Domains.Compromise.Domain;
using Domains.Compromise.Infrastructure.EntityFramework;

namespace Domains.Compromise.Infrastructure
{
    public class OrderRepository
    {
        public Order Get(Guid id)
        {
            return null;
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
