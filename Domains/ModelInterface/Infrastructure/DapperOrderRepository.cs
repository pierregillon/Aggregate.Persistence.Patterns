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
                string query = @"SELECT Id, OrderStatus, TotalCost, SubmitDate FROM [dbo].[Order] WHERE Id = @id 
                                 SELECT CreationDate, Product, Quantity FROM [dbo].[OrderLine] WHERE OrderId = @id";

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
                connection.Execute(@"INSERT INTO [dbo].[Order] (Id, OrderStatus, TotalCost, SubmitDate) VALUES(@Id, @OrderStatus, @TotalCost, @SubmitDate)", persistentModel);
                connection.Execute(@"INSERT INTO [dbo].[OrderLine] (CreationDate, Product, Quantity, OrderId) VALUES(@CreationDate, @Product, @Quantity, @OrderId)",
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