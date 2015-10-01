using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domains.Compromise.Domain;

namespace Domains.Compromise.Infrastructure
{
    public class DapperOrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            using (var connection = new SqlConnection(@"Server=localhost\\SQLEXPRESS;database=DomainModelPatterns.Compromise;")) {
                return connection.Query<Order>("SELECT * FROM Order WHERE Id = @id", new {id}).FirstOrDefault();
            }
        }
        public void Add(Order order)
        {
            using (var connection = new SqlConnection(@"Server=localhost\\SQLEXPRESS;database=DomainModelPatterns.Compromise;")) {
                connection.Execute(@"INSERT INTO Order (Id, OrderStatus, TotalCost, SubmitDate) VALUES(@a, @b, @c, @d)", new
                {
                    a = order.Id,
                    b = order.OrderStatus,
                    c = order.TotalCost,
                    d = order.SubmitDate
                });
            }
        }
    }
}