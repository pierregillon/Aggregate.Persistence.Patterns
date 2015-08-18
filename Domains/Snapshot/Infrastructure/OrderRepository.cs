using System;
using Domains.Snapshot.Domain;
using Domains.Snapshot.Infrastructure.EntityFramework;

namespace Domains.Snapshot.Infrastructure
{
    public class OrderRepository
    {
        public Order Get(Guid id)
        {
            return null;
        }

        public void Add(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
            using (var dataContext = new DataContext()) {
                dataContext.Set<OrderState>().Add(orderState);
                dataContext.SaveChanges();
            }
        }
    }
}
