using System;
using System.Linq;
using Domains.ModelInterface.Domain;
using Domains.ModelInterface.Infrastructure.EntityFramework;
using Order = Domains.ModelInterface.Domain.Order;

namespace Domains.ModelInterface.Infrastructure
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
    }
}
