using System;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public interface IOrderLineStates
    {
        Guid OrderId { get; set; }
        Product Product { get; set; }
        int Quantity { get; set; }
        DateTime CreationDate { get; set; }
    }
}