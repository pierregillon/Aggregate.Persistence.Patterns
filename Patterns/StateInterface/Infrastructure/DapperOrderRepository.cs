﻿using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Patterns.Common.Infrastructure;
using Patterns.StateInterface.Domain;

namespace Patterns.StateInterface.Infrastructure
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

                    if (persistentModel == null) {
                        return null;
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
                connection.Execute(SqlQueries.InsertOrderLineQuery, persistentModel.Lines);
            }
        }

        public void Update(Order order)
        {
            var persistentModel = new OrderPersistantModel();
            order.CopyTo(persistentModel);
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.UpdateOrderQuery, persistentModel);
                connection.Execute(SqlQueries.DeleteOrderLineQuery, new {OrderId = persistentModel.Id});
                connection.Execute(SqlQueries.InsertOrderLineQuery, persistentModel.Lines);
            }
        }

        public void Delete(Guid orderId)
        {
            using (var connection = new SqlConnection(SqlConnectionLocator.LocalhostSqlExpress())) {
                connection.Execute(SqlQueries.DeleteOrderLineQuery, new {OrderId = orderId});
                connection.Execute(SqlQueries.DeleteOrderQuery, new {OrderId = orderId});
            }
        }
    }
}