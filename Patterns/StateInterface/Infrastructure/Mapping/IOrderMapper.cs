using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public interface IOrderMapper
    {
        Order ToDomainModel(OrderPersistentModel persistentModel);
        OrderPersistentModel ToPersistentModel(Order domainModel);
    }
}