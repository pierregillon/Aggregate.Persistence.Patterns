using System;
using System.Data.Entity;
using System.Linq;
using Patterns.StateInterface.Domain;
using Patterns.StateInterface.Infrastructure.EntityFramework;

namespace Patterns.StateInterface.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                var orderState = dataContext
                    .Set<OrderPersistantModel>()
                    .Include("Lines")
                    .FirstOrDefault(x => x.Id == id);

                if (orderState == null) {
                    return null;
                }
                var order = new Order();
                orderState.CopyTo(order);
                return order;
            }
        }

        public void Add(Order order)
        {
            var persistantModel = new OrderPersistantModel();
            order.CopyTo(persistantModel);
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderPersistantModel>().Add(persistantModel);
                dataContext.SaveChanges();
            }
        }

        public void Update(Order order)
        {
            var persistantModel = new OrderPersistantModel();
            order.CopyTo(persistantModel);
            using (var dataContext = new DataContext()) {
                dataContext.Entry(persistantModel).State = EntityState.Modified;
                persistantModel.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Guid orderId)
        {
            using (var dataContext = new DataContext()) {
                var order = dataContext.Set<OrderPersistantModel>().Find(orderId);
                dataContext.Entry(order).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }
    }
}