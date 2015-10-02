using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain.Base.Infrastructure;
using Domains.ModelInterface.Domain;

namespace Domains.ModelInterface.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                const string query = SqlQueries.SelectOrdersByIdQuery + " " + SqlQueries.SelectOrderLinesByIdQuery;
                using (var multi = connection.QueryMultiple(query, new {id})) {
                    var persistentModel = multi.Read<OrderPersistantModel>().SingleOrDefault();
                    if (persistentModel != null) {
                        persistentModel.Lines = multi.Read<OrderLinePersistantModel>().ToList();
                    }

                    var order = new Order();
                    persistentModel.CopyTo(order);
                    return order;
                }
            }
        }

        public void Add(Order order)
        {
            var persistentModel = new OrderPersistantModel();
            order.CopyTo(persistentModel);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderQuery, persistentModel);
                connection.Execute(SqlQueries.InsertOrderLineQuery,
                    persistentModel.Lines.Select(x => new
                    {
                        x.CreationDate,
                        x.Product,
                        x.Quantity,
                        OrderId = persistentModel.Id
                    }));
            }
        }
    }
}