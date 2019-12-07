using System;
using System.Data.SqlClient;
using System.Linq;
using Aggregate.Persistence.StateInterface.Domain;
using Aggregate.Persistence.StateInterface.Infrastructure.Mapping;
using Common.Infrastructure;
using Dapper;

namespace Aggregate.Persistence.StateInterface.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        private readonly IOrderMapper _orderMapper;

        public DapperOrderRepository(IOrderMapper orderMapper)
        {
            _orderMapper = orderMapper;
        }

        public Order Get(Guid id)
        {
            using var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress());
            const string query = SqlQueries.SelectOrdersByIdQuery + " " + SqlQueries.SelectOrderLinesByIdQuery;
            using var multi = connection.QueryMultiple(query, new { id });
            var persistentModel = multi.Read<OrderPersistentModel>().SingleOrDefault();
            if (persistentModel == null) {
                return null;
            }

            persistentModel.Lines = multi.Read<OrderLinePersistentModel>().ToList();
            persistentModel.Lines.ForEach(x => x.OrderId = id);
            return _orderMapper.ToDomainModel(persistentModel);
        }

        public void Add(Order order)
        {
            var persistentModel = _orderMapper.ToPersistentModel(order);
            using var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress());
            connection.Execute(SqlQueries.InsertOrderQuery, persistentModel);
            connection.Execute(SqlQueries.InsertOrderLineQuery, persistentModel.Lines);
        }

        public void Update(Order order)
        {
            var persistentModel = _orderMapper.ToPersistentModel(order);
            using var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress());
            connection.Execute(SqlQueries.UpdateOrderQuery, persistentModel);
            connection.Execute(SqlQueries.DeleteOrderLineQuery, new { OrderId = persistentModel.Id });
            connection.Execute(SqlQueries.InsertOrderLineQuery, persistentModel.Lines);
        }

        public void Delete(Guid orderId)
        {
            using var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress());
            connection.Execute(SqlQueries.DeleteOrderLineQuery, new { OrderId = orderId });
            connection.Execute(SqlQueries.DeleteOrderQuery, new { OrderId = orderId });
        }
    }
}