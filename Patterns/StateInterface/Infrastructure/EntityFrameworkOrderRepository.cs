using System;
using System.Data.Entity;
using System.Linq;
using Patterns.StateInterface.Domain;
using Patterns.StateInterface.Infrastructure.EntityFramework;
using Patterns.StateInterface.Infrastructure.Mapping;

namespace Patterns.StateInterface.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        private readonly IOrderMapper _orderMapper;

        public EntityFrameworkOrderRepository(IOrderMapper orderMapper)
        {
            _orderMapper = orderMapper;
        }

        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                var persistentModel = dataContext
                    .Set<OrderPersistantModel>()
                    .Include("Lines")
                    .FirstOrDefault(x => x.Id == id);

                if (persistentModel == null) {
                    return null;
                }
                return _orderMapper.ToDomainModel(persistentModel);
            }
        }

        public void Add(Order order)
        {
            var persistantModel = _orderMapper.ToPersistentModel(order);
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderPersistantModel>().Add(persistantModel);
                dataContext.SaveChanges();
            }
        }

        public void Update(Order order)
        {
            var persistantModel = _orderMapper.ToPersistentModel(order);
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