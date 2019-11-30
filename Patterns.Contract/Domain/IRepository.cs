using System;

namespace Patterns.Contract.Domain
{
    public interface IRepository<TOrder> where TOrder : IOrder
    {
        TOrder Get(Guid id);
        void Add(TOrder order);
        void Update(TOrder order);
        void Delete(Guid orderId);
    }
}