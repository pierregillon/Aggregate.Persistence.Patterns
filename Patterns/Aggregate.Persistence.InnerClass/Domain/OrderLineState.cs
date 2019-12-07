using System;
using Common.Domain;

namespace Aggregate.Persistence.InnerClass.Domain
{
    public class OrderLineState
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

        // EF properties
        public Guid OrderId { get; set; }
    }
}