using System;
using System.Collections.Generic;
using Common.Domain;

namespace Aggregate.Persistence.StateSnapshot.Domain
{
    public class OrderState
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double TotalCost { get; set; }
        public List<OrderLineState> Lines { get; set; }

        public OrderState()
        {
            Lines = new List<OrderLineState>();
        }
    }
}