using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Domains.ModelInterface.Domain;

namespace Domains.ModelInterface.Infrastructure
{
    public class OrderPersistantModel : IOrderStates
    {
        ICollection<IOrderLineStates> IOrderStates.Lines
        {
            get { return Lines.OfType<IOrderLineStates>().ToList(); }
            set
            {
                Lines.Clear();
                foreach (var orderLine in value) {
                    var persistantOrderLine = new OrderLinePersistantModel();
                    orderLine.CopyTo(persistantOrderLine);
                    Lines.Add(persistantOrderLine);
                }
            }
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