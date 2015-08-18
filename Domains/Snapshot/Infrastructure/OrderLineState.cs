using System;
using ClassLibrary1;
using Domains.Snapshot.Domain;

namespace Domains.Snapshot.Infrastructure
{
    public class OrderLineState
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public OrderState Order { get; set; }
    }
}