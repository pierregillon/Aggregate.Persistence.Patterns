using Aggregate.Persistence.StateInterface.Domain;

namespace Aggregate.Persistence.StateInterface.Infrastructure.Mapping
{
    public interface IOrderMapper : IMapper<Order, OrderPersistentModel> { }
}