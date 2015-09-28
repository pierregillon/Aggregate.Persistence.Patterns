using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public interface IOrderPersistantModel
    {
        Guid Id { get; set; }
        OrderStatus OrderStatus { get; set; }
        DateTime? SubmitDate { get; set; }
        double TotalCost { get; set; }
        ICollection<IOrderLinePersistantModel> Lines { get; set; }
    }
}