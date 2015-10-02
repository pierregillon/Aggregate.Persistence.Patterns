using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Common;
using Patterns.ModelInterface.Domain;

namespace Patterns.ModelInterface.Infrastructure
{
    public class OrderPersistantModel : IOrderStates<OrderLinePersistantModel>
    {
        IEnumerable<OrderLinePersistantModel> IOrderStates<OrderLinePersistantModel>.Lines
        {
            get
            {
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
        public List<OrderLinePersistantModel> Lines { get; set; }

        public OrderPersistantModel()
        {
            Lines = new List<OrderLinePersistantModel>();
        }
    }
}