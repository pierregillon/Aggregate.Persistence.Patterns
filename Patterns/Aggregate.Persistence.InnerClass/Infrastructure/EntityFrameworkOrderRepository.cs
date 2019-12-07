using System;
using System.Data.Entity;
using System.Linq;
using Aggregate.Persistence.InnerClass.Domain;
using Aggregate.Persistence.InnerClass.Infrastructure.EntityFramework;

namespace Aggregate.Persistence.InnerClass.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using var dataContext = new DataContext();
            var orderState = dataContext
                .Set<OrderState>()
                .Include("Lines")
                .FirstOrDefault(x => x.Id == id);

            if (orderState == null) {
                return null;
            }

            return new Order.FromState().Build(orderState);
        }

        public void Add(Order order)
        {
            var orderState = new Order.ToState().Build(order);
            using var dataContext = new DataContext();
            dataContext.Set<OrderState>().Add(orderState);
            dataContext.SaveChanges();
        }

        public void Update(Order order)
        {
            var orderState = new Order.ToState().Build(order);
            using var dataContext = new DataContext();
            dataContext.Entry(orderState).State = EntityState.Modified;
            orderState.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
            dataContext.SaveChanges();
        }

        public void Delete(Guid orderId)
        {
            using var dataContext = new DataContext();
            var orderState = dataContext.Set<OrderState>().Find(orderId);
            dataContext.Entry(orderState).State = EntityState.Deleted;
            orderState.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
            dataContext.SaveChanges();
        }
    }
}