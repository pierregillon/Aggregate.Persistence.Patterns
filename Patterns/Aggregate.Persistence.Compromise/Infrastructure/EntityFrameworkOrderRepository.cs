using System;
using System.Data.Entity;
using System.Linq;
using Aggregate.Persistence.Compromise.Domain;
using Aggregate.Persistence.Compromise.Infrastructure.EntityFramework;

namespace Aggregate.Persistence.Compromise.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using var dataContext = new DataContext();
            return dataContext
                .Set<Order>()
                .Include("Lines")
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Order order)
        {
            using var dataContext = new DataContext();
            dataContext.Set<Order>().Add(order);
            dataContext.SaveChanges();
        }

        public void Update(Order order)
        {
            using var dataContext = new DataContext();
            dataContext.Entry(order).State = EntityState.Modified;
            order.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
            dataContext.SaveChanges();
        }

        public void Delete(Guid orderId)
        {
            using var dataContext = new DataContext();
            var order = dataContext.Set<Order>().Find(orderId);
            dataContext.Entry(order).State = EntityState.Deleted;
            dataContext.SaveChanges();
        }
    }
}