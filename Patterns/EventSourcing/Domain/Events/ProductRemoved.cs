using System;
using Patterns.Contract.Domain;

namespace Patterns.EventSourcing.Domain.Events
{
    public class ProductRemoved : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Product Product { get; set; }

        public ProductRemoved(Guid id, Product product)
        {
            AggregateId = id;
            Product = product;
        }
    }
}