using System;
using Aggregate.Persistence.EventSourcing.Domain.Base;
using Common.Domain;

namespace Aggregate.Persistence.EventSourcing.Domain.Events
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