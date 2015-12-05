using Patterns.Common;
using Patterns.Common.Domain;
using Patterns.InnerClass.Domain;

namespace Patterns.InnerClass.Infrastructure
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}