using System;
using Patterns.Contract.Domain;

namespace Patterns.EventSourcing.Domain.Events
{
    public class ProductAdded : IDomainEvent
    {
        public Guid AggregateId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreationDate { get; set; }

        public ProductAdded(Guid aggregateId, Product product, int quantity, DateTime creationDate)
        {
            AggregateId = aggregateId;
            Product = product;
            Quantity = quantity;
            CreationDate = creationDate;
        }
    }
}