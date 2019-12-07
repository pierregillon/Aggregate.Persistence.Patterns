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
                cfg.CreateMap<IOrderStates<OrderLine>, IOrderStates<OrderLinePersistentModel>>().AfterMap((domainModel, persistentModel) => {
                    foreach (var line in persistentModel.Lines) {
                        line.OrderId = domainModel.Id;
                    }
                });
                cfg.CreateMap<IOrderStates<OrderLinePersistentModel>, IOrderStates<OrderLine>>();
                cfg.CreateMap<IOrderLineStates, OrderLinePersistentModel>();
                cfg.CreateMap<IOrderLineStates, OrderLine>();
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