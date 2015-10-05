using System;
using Patterns.Common;

namespace Patterns.StateInterface.Domain
{
    public interface IOrderLineStates
    {
        Guid OrderId { get; set; }
        Product Product { get; set; }
        int Quantity { get; set; }
        DateTime CreationDate { get; set; }
    }
}