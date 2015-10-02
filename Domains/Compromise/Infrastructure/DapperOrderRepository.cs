using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain.Base.Infrastructure;
using Domains.Compromise.Domain;

namespace Domains.Compromise.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress()))
            {
                string query = @"SELECT Id, OrderStatus, TotalCost, SubmitDate FROM [dbo].[Order] WHERE Id = @id;
                                 SELECT CreationDate, Product, Quantity FROM [dbo].[OrderLine] WHERE OrderId = @id";

                using (var multi = connection.QueryMultiple(query, new{id}))
                {
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
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress()))
            {
                connection.Execute(@"INSERT INTO [dbo].[Order] (Id, OrderStatus, TotalCost, SubmitDate) VALUES(@Id, @OrderStatus, @TotalCost, @SubmitDate)", order);
                connection.Execute(@"INSERT INTO [dbo].[OrderLine] (CreationDate, Product, Quantity, OrderId) VALUES(@CreationDate, @Product, @Quantity, @OrderId)", order.Lines);
            }
        }
    }
}