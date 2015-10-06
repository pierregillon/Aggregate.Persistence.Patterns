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
                    .Set<OrderPersistentModel>()
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
            var persistentModel = _orderMapper.ToPersistentModel(order);
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderPersistentModel>().Add(persistentModel);
                dataContext.SaveChanges();
            }
        }

        public void Update(Order order)
        {
            var persistentModel = _orderMapper.ToPersistentModel(order);
            using (var dataContext = new DataContext()) {
                dataContext.Entry(persistentModel).State = EntityState.Modified;
                persistentModel.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Guid orderId)
        {
            using (var dataContext = new DataContext()) {
                var order = dataContext.Set<OrderPersistentModel>().Find(orderId);
                dataContext.Entry(order).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }
    }
}