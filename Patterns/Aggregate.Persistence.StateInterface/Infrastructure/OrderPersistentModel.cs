using System;
using System.Collections.Generic;
using System.Linq;
using Aggregate.Persistence.StateInterface.Domain;
using Common.Domain;

namespace Aggregate.Persistence.StateInterface.Infrastructure
{
    public class OrderPersistentModel : IOrderStates<OrderLinePersistentModel>
    {
        IEnumerable<OrderLinePersistentModel> IOrderStates<OrderLinePersistentModel>.Lines
        {
            get {
                // We do a copy of the origin list to avoid
                // modification from outside.
                return Lines.ToArray();
            }
            set { Lines = value.ToList(); }
        }

        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double TotalCost { get; set; }
        public List<OrderLinePersistentModel> Lines { get; set; }

        public OrderPersistentModel()
        {
            Lines = new List<OrderLinePersistentModel>();
        }
    }
}