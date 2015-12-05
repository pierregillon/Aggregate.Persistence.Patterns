using System;
using Patterns.Common;
using Patterns.Common.Domain;

namespace Patterns.StateInterface.Domain
{
    public interface IOrderLineStates
    {
        Product Product { get; set; }
        int Quantity { get; set; }
        DateTime CreationDate { get; set; }
    }
}