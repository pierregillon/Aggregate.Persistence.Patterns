using System;
using System.Collections.Generic;
using Common.Domain;

namespace Aggregate.Persistence.StateInterface.Domain
{
    public interface IOrderStates<TOrderLine> where TOrderLine : IOrderLineStates
    {
        Guid Id { get; set; }
        OrderStatus OrderStatus { get; set; }
        DateTime? SubmitDate { get; set; }
        double TotalCost { get; set; }
        IEnumerable<TOrderLine> Lines { get; set; }
    }
}