using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Aggregate.Persistence.Binary.Domain;

namespace Aggregate.Persistence.Binary.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public Order Get(Guid id)
        {
            if (!File.Exists(GetFilePath(id))) {
                return null;
            }

            using var stream = File.OpenRead(GetFilePath(id));
            var formatter = new BinaryFormatter();
            return (Order) formatter.Deserialize(stream);
        }

        public void Add(Order order)
        {
            using var stream = File.Create(GetFilePath(order.Id));
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, order);
        }

        public void Update(Order order)
        {
            Delete(order.Id);
            Add(order);
        }

        public void Delete(Guid orderId)
        {
            if (File.Exists(GetFilePath(orderId))) {
                File.Delete(GetFilePath(orderId));
            }
        }

        private static string GetFilePath(Guid id)
        {
            return id + ".save.binary";
        }
    }
}