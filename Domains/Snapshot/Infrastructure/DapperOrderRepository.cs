using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain.Base.Infrastructure;
using Domains.Snapshot.Domain;

namespace Domains.Snapshot.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                string query = @"SELECT Id, OrderStatus, TotalCost, SubmitDate FROM Compromise_Order WHERE Id = @id 
                                 SELECT CreationDate, Product, Quantity FROM Compromise_OrderLine WHERE OrderId = @id";

                using (var multi = connection.QueryMultiple(query, new {id})) {
                    var orderState = multi.Read<OrderState>().SingleOrDefault();
                    if (orderState != null) {
                        orderState.Lines = multi.Read<OrderLineState>().ToList();
                    }

                    var order = new Order();
                    ((IStateSnapshotable<OrderState>) order).LoadFromSnapshot(orderState);
                    return order;
                }
            }
        }
        public void Add(Order order)
        {
            var orderState = ((IStateSnapshotable<OrderState>) order).TakeSnapshot();
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(@"INSERT INTO Compromise_Order (Id, OrderStatus, TotalCost, SubmitDate) VALUES(@Id, @OrderStatus, @TotalCost, @SubmitDate)", orderState);
                connection.Execute(@"INSERT INTO Compromise_OrderLine (CreationDate, Product, Quantity, OrderId) VALUES(@CreationDate, @Product, @Quantity, @OrderId)", 
                    orderState.Lines.Select(x=>new
                    {
                        x.CreationDate,
                        x.Product,
                        x.Quantity,
                        OrderId = orderState.Id
                    }));
            }
        }
    }
}