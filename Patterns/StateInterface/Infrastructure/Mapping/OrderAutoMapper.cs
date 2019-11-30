using AutoMapper;
using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public class OrderAutoMapper : IOrderMapper
    {
        private readonly IMapper _mapper;

        public OrderAutoMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<IOrderLineStates, IOrderLineStates>();
                cfg.CreateMap<IOrderLineStates, OrderLinePersistentModel>().As<IOrderLineStates>();
                cfg.CreateMap<IOrderLineStates, OrderLine>().As<IOrderLineStates>();
                cfg.CreateMap<IOrderStates<OrderLinePersistentModel>, IOrderStates<OrderLine>>();
                cfg.CreateMap<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistentModel>>().AfterMap((domainModel, persistentModel) => {
                    foreach (var line in persistentModel.Lines) {
                        line.OrderId = domainModel.Id;
                    }
                });
            });

            _mapper = config.CreateMapper();
        }

        public Order ToDomainModel(OrderPersistentModel persistentModel)
        {
            var order = new Order();
            _mapper.Map<IOrderStates<OrderLinePersistentModel>, IOrderStates<OrderLine>>(persistentModel, order);
            return order;
        }

        public OrderPersistentModel ToPersistentModel(Order domainModel)
        {
            var persistentModel = new OrderPersistentModel();
            _mapper.Map<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistentModel>>(domainModel, persistentModel);
            return persistentModel;
        }
    }
}