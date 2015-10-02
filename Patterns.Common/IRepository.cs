using System;

namespace Patterns.Common
{
    public interface IRepository<TOrder>
    {
        TOrder Get(Guid id);
        void Add(TOrder order);
        void Update(TOrder order);
    }
}