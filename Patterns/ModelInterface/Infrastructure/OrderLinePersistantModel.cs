using System;
using Patterns.Common;
using Patterns.ModelInterface.Domain;

namespace Patterns.ModelInterface.Infrastructure
{
    public class OrderLinePersistantModel : IOrderLineStates
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

        // EF properties
        public Guid OrderId { get; set; }
    }
}