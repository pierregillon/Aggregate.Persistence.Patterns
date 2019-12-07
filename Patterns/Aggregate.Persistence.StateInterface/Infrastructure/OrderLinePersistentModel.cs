using System;
using Aggregate.Persistence.StateInterface.Domain;
using Common.Domain;

namespace Aggregate.Persistence.StateInterface.Infrastructure
{
    public class OrderLinePersistentModel : IOrderLineStates
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

        // EF properties
        public Guid OrderId { get; set; }
    }
}