using System;

namespace Domain.Base
{
    public interface IRepository<TOrder>
    {
        TOrder Get(Guid id);
        void Add(TOrder order);
    }
}