using System;
using Domain.Base;
using Domains.ModelInterface.Domain;

namespace Domains.ModelInterface.Infrastructure
{
    public class PersistantOrderLine : IOrderLinePersistantModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }

        public PersistantOrder Order { get; set; }
    }
}