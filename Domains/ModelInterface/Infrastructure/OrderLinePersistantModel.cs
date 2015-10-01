using System;
using Domain.Base;
using Domains.ModelInterface.Domain;

namespace Domains.ModelInterface.Infrastructure
{
    public class OrderLinePersistantModel : IOrderLineStates
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid OrderId { get; set; }

        public OrderPersistantModel Order { get; set; }
    }
}