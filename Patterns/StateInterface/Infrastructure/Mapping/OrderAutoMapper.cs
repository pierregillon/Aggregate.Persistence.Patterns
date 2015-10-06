using AutoMapper;
using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public class OrderAutoMapper : IOrderMapper
    {
        public OrderAutoMapper()
        {
            Mapper.CreateMap<IOrderLineStates, IOrderLineStates>();
            Mapper.CreateMap<IOrderLineStates, OrderLinePersistentModel>().As<IOrderLineStates>();
            Mapper.CreateMap<IOrderLineStates, OrderLine>().As<IOrderLineStates>();

            Mapper.CreateMap<IOrderStates<OrderLinePersistentModel>, IOrderStates<OrderLine>>();
            Mapper.CreateMap<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistentModel>>().AfterMap((domainModel, persistentModel) =>
            {
                foreach (var line in persistentModel.Lines) {
                    line.OrderId = domainModel.Id;
                }
            });
        }

        public Order ToDomainModel(OrderPersistentModel persistentModel)
        {
            var order = new Order();
            Mapper.Map<IOrderStates<OrderLinePersistentModel>, IOrderStates<OrderLine>>(persistentModel, order);
            return order;
        }

        public OrderPersistentModel ToPersistentModel(Order domainModel)
        {
            var persistentModel = new OrderPersistentModel();
            Mapper.Map<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistentModel>>(domainModel, persistentModel);
            return persistentModel;
        }
    }
}