using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Patterns.Binary.Domain;

namespace Patterns.Binary.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private const string FilePath = "save.binary";

        public Order Get(Guid id)
        {
            using (var stream = File.OpenRead(FilePath)) {
                var formatter = new BinaryFormatter();
                return (Order) formatter.Deserialize(stream);
            }
        }

        public void Add(Order order)
        {
            using (var stream = File.Create(FilePath)) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, order);
            }
        }
    }
}