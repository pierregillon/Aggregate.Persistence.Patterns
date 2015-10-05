using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Patterns.Common.Infrastructure;
using Patterns.Compromise.Domain;

namespace Patterns.Compromise.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                const string query = SqlQueries.SelectOrdersByIdQuery + " " + SqlQueries.SelectOrderLinesByIdQuery;
                using (var multi = connection.QueryMultiple(query, new {id})) {
                    var order = multi.Read<Order>().SingleOrDefault();
                    if (order != null) {
                        order.Lines = multi.Read<OrderLine>().ToList();
                    }
                    return order;
                }
            }
        }

        public void Add(Order order)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderQuery, order);
                connection.Execute(SqlQueries.InsertOrderLineQuery, order.Lines);
            }
        }
        public void Update(Order order)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.UpdateOrderQuery, order);
                connection.Execute(SqlQueries.DeleteOrderLineQuery, new {OrderId = order.Id});
                connection.Execute(SqlQueries.InsertOrderLineQuery, order.Lines);
            }
        }
    }
}