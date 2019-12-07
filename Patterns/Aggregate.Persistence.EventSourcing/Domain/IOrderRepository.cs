using Patterns.Contract.Domain;

namespace Patterns.EventSourcing.Domain
{
    public interface IOrderRepository : IRepository<Order> { }
}