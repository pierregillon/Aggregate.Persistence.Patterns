using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public interface IOrderStates
    {
        Guid Id { get; set; }
        OrderStatus OrderStatus { get; set; }
        DateTime? SubmitDate { get; set; }
        double TotalCost { get; set; }
        ICollection<IOrderLineStates> Lines { get; set; }
    }
}