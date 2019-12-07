using System;
using Common.Domain;

namespace Aggregate.Persistence.StateInterface.Domain
{
    public interface IOrderLineStates
    {
        Product Product { get; set; }
        int Quantity { get; set; }
        DateTime CreationDate { get; set; }
    }
}