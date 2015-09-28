using System;
using System.Linq;
using Domain.Base;
using Domains.ModelInterface.Domain;
using Domains.ModelInterface.Infrastructure.EntityFramework;
using Order = Domains.ModelInterface.Domain.Order;

namespace Domains.ModelInterface.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                var orderState = dataContext
                    .Set<PersistantOrder>()
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
            var persistantModel = new PersistantOrder();
            order.CopyTo(persistantModel);
            using (var dataContext = new DataContext()) {
                dataContext.Set<PersistantOrder>().Add(persistantModel);
                dataContext.SaveChanges();
            }
        }
    }
}
