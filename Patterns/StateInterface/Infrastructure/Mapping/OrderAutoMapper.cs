using AutoMapper;
using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public class OrderAutoMapper : IOrderMapper
    {
        public OrderAutoMapper()
        {
            Mapper.CreateMap<IOrderLineStates, IOrderLineStates>();
            Mapper.CreateMap<IOrderLineStates, OrderLinePersistantModel>().As<IOrderLineStates>();
            Mapper.CreateMap<IOrderLineStates, OrderLine>().As<IOrderLineStates>();

            Mapper.CreateMap<IOrderStates<OrderLinePersistantModel>, IOrderStates<OrderLine>>();
            Mapper.CreateMap<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistantModel>>();
        }

        public Order ToDomainModel(OrderPersistantModel persistentModel)
        {
            var order = new Order();
            Mapper.Map<IOrderStates<OrderLinePersistantModel>, IOrderStates<OrderLine>>(persistentModel, order);
            return order;
        }

        public OrderPersistantModel ToPersistentModel(Order domainModel)
        {
            var persistentModel = new OrderPersistantModel();
            Mapper.Map<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistantModel>>(domainModel, persistentModel);
            return persistentModel;
        }
    }
}