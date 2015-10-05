using System;
using System.Data.Entity;
using System.Linq;
using Patterns.Snapshot.Domain;
using Patterns.Snapshot.Infrastructure.EntityFramework;

namespace Patterns.Snapshot.Infrastructure
{
    public class EntityFrameworkOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var dataContext = new DataContext()) {
                var orderState = dataContext
                    .Set<OrderState>()
                    .Include("Lines")
                    .FirstOrDefault(x => x.Id == id);

                if (orderState == null) {
                    return null;
                }
                var order = new Order();
                ((IStateSnapshotable<OrderState>) order).LoadFromSnapshot(orderState);
                return order;
            }
        }

        public void Add(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderState>().Add(orderState);
                dataContext.SaveChanges();
            }
        }

        public void Update(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
            using (var dataContext = new DataContext()) {
                dataContext.Entry(orderState).State = EntityState.Modified;
                orderState.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Guid orderId)
        {
            using (var dataContext = new DataContext()) {
                var orderState = dataContext.Set<OrderState>().Find(orderId);
                dataContext.Entry(orderState).State = EntityState.Deleted;
                orderState.Lines.ForEach(x => dataContext.Entry(x).State = EntityState.Added);
                dataContext.SaveChanges();
            }
        }
    }
}