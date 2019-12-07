using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public interface IOrderMapper : IMapper<Order, OrderPersistentModel> { }
}