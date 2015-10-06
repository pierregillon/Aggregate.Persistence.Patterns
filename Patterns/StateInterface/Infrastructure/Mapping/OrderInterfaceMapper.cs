using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public class OrderInterfaceMapper : IOrderMapper
    {
        public Order ToDomainModel(OrderPersistantModel persistentModel)
        {
            var order = new Order();
            persistentModel.CopyTo(order);
            return order;
        }

        public OrderPersistantModel ToPersistentModel(Order domainModel)
        {
            var persistentModel = new OrderPersistantModel();
            domainModel.CopyTo(persistentModel);
            return persistentModel;
        }
    }
}