using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public interface IOrderMapper
    {
        Order ToDomainModel(OrderPersistantModel persistentModel);
        OrderPersistantModel ToPersistentModel(Order domainModel);
    }
}