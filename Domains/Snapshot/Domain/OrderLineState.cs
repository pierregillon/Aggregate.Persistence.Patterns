using System;
using Domain.Base;

namespace Domains.Snapshot.Domain
{
    public class OrderLineState
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public OrderState Order { get; set; }
    }
}