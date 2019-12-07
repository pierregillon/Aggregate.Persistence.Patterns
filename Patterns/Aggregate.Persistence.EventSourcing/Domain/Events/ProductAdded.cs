using System;
using Patterns.Contract.Domain;
using Patterns.EventSourcing.Domain.Base;

namespace Patterns.EventSourcing.Domain.Events
{
    public class ProductAdded : IDomainEvent
    {
        public Guid AggregateId { get; }
        public Product Product { get; }
        public int Quantity { get; }
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