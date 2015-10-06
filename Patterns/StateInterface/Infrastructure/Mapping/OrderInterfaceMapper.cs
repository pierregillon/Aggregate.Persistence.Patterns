using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public class OrderInterfaceMapper : IOrderMapper
    {
        public Order ToDomainModel(OrderPersistentModel persistentModel)
        {
            var order = new Order();
            persistentModel.CopyTo(order);
            return order;
        }

        public OrderPersistentModel ToPersistentModel(Order domainModel)
        {
            var persistentModel = new OrderPersistentModel();
            domainModel.CopyTo(persistentModel);
            foreach (var line in persistentModel.Lines) {
                line.OrderId = persistentModel.Id;
            }
            return persistentModel;
        }
    }
}