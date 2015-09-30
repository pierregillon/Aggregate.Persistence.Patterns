using System;
using Domain.Base;

namespace Domains.EventSourcing.Domain.Events
{
    public class ProductAdded : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public ProductAdded(Guid aggregateId, Product product, int quantity)
        {
            AggregateId = aggregateId;
            Product = product;
            Quantity = quantity;
        }
    }
}