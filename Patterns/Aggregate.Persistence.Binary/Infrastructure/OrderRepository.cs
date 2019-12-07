using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Aggregate.Persistence.Binary.Domain;

namespace Aggregate.Persistence.Binary.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private const string FilePath = "save.binary";

        public Order Get(Guid id)
        {
            if (!File.Exists(FilePath)) {
                return null;
            }

            using var stream = File.OpenRead(FilePath);
            var formatter = new BinaryFormatter();
            return (Order) formatter.Deserialize(stream);
        }

        public void Add(Order order)
        {
            using var stream = File.Create(FilePath);
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
            if (File.Exists(FilePath)) {
                File.Delete(FilePath);
            }
        }
    }
}