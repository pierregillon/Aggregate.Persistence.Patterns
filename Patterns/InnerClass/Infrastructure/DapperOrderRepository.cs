using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Patterns.Common.Infrastructure;
using Patterns.InnerClass.Domain;

namespace Patterns.InnerClass.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                const string query = SqlQueries.SelectOrdersByIdQuery + " " + SqlQueries.SelectOrderLinesByIdQuery;
                using (var multi = connection.QueryMultiple(query, new {id})) {
                    var orderState = multi.Read<OrderState>().SingleOrDefault();
                    if (orderState == null) {
                        return null;
                    }
                    
                    orderState.Lines = multi.Read<OrderLineState>().ToList();

                    return new Order.FromState().Build(orderState);
                }
            }
        }

        public void Add(Order order)
        {
            var orderState = new Order.ToState().Build(order);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderQuery, orderState);
                connection.Execute(SqlQueries.InsertOrderLineQuery, orderState.Lines);
            }
        }

        public void Update(Order order)
        {
            var orderState = new Order.ToState().Build(order);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.UpdateOrderQuery, orderState);
                connection.Execute(SqlQueries.DeleteOrderLineQuery, new {OrderId = orderState.Id});
                connection.Execute(SqlQueries.InsertOrderLineQuery, orderState.Lines);
            }
        }

        public void Delete(Guid orderId)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.DeleteOrderLineQuery, new { OrderId = orderId });
                connection.Execute(SqlQueries.DeleteOrderQuery, new { OrderId = orderId });
            }
        }
    }
}