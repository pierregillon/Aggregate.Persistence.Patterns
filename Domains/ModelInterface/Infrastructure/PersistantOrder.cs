using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;
using Domains.ModelInterface.Domain;

namespace Domains.ModelInterface.Infrastructure
{
    public class PersistantOrder : IOrderPersistantModel
    {
        ICollection<IOrderLinePersistantModel> IOrderPersistantModel.Lines
        {
            get { return Lines.OfType<IOrderLinePersistantModel>().ToList(); }
            set
            {
                Lines.Clear();
                foreach (var orderLine in value) {
                    var persistantOrderLine = new PersistantOrderLine();
                    orderLine.CopyTo(persistantOrderLine);
                    Lines.Add(persistantOrderLine);
                }
            }
        }

        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double TotalCost { get; set; }
        public List<PersistantOrderLine> Lines { get; set; }

        public PersistantOrder()
        {
            Lines = new List<PersistantOrderLine>();
        }
    }
}