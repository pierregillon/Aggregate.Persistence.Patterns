using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Patterns.Common.Infrastructure;
using Patterns.StateSnapshot.Domain;

namespace Patterns.StateSnapshot.Infrastructure
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
                    
                    var order = new Order();
                    ((IStateSnapshotable<OrderState>) order).LoadSnapshot(orderState);
                    return order;
                }
            }
        }

        public void Add(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.InsertOrderQuery, orderState);
                connection.Execute(SqlQueries.InsertOrderLineQuery, orderState.Lines);
            }
        }

        public void Update(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
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