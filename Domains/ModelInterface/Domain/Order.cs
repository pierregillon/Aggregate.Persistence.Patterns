using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public class Order : IOrder, IOrderPersistantModel
    {
        Guid IOrderPersistantModel.Id
        {
            get { return Id; }
            set { Id = value; }
        }
        OrderStatus IOrderPersistantModel.OrderStatus
        {
            get { return OrderStatus; }
            set { OrderStatus = value; }
        }
        DateTime? IOrderPersistantModel.SubmitDate
        {
            get { return SubmitDate; }
            set { SubmitDate = value; }
        }
        double IOrderPersistantModel.TotalCost
        {
            get { return TotalCost; }
            set { TotalCost = value; }
        }
        ICollection<IOrderLinePersistantModel> IOrderPersistantModel.Lines
        {
            get { return _lines.OfType<IOrderLinePersistantModel>().ToList(); }
            set
            {
                _lines.Clear();
                foreach (var persistantModel in value) {
                    var orderLine = new OrderLine();
                    persistantModel.CopyTo(orderLine);
                    _lines.Add(orderLine);
                }
            }
        }


        private readonly ProductCatalog _catalog = new ProductCatalog();
        private readonly List<OrderLine> _lines = new List<OrderLine>();

        public Guid Id { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime? SubmitDate { get; private set; }
        public double TotalCost { get; private set; }

        // ----- Constructor
        public Order()
        {
            Id = Guid.NewGuid();
        }

        // ----- Public methods
        public void AddProduct(Product product, int quantity)
        {
            CheckIfDraft();

            var line = _lines.FirstOrDefault(x => x.Product == product);
            if (line == null) {
                _lines.Add(new OrderLine(product, quantity));
            }
            else {
                line.IncreaseQuantity(quantity);
            }

            ReCalculateTotalPrice();
        }
        public void RemoveProduct(Product product)
        {
            CheckIfDraft();

            var line = _lines.FirstOrDefault(x => x.Product == product);
            if (line != null) {
                _lines.Remove(line);
            }

            ReCalculateTotalPrice();
        }
        public int GetQuantity(Product product)
        {
            var line = _lines.FirstOrDefault(x => x.Product == product);
            if (line == null) {
                return 0;
            }
            return line.Quantity;
        }
        public void Submit()
        {
            CheckIfDraft();
            SubmitDate = DateTime.Now;
            OrderStatus = OrderStatus.Submitted;
        }

        // ----- Internal logic
        private void CheckIfDraft()
        {
            if (OrderStatus != OrderStatus.Draft)
                throw new OrderOperationException("The operation is only allowed if the order is in draft state.");
        }
        private void ReCalculateTotalPrice()
        {
            if (_lines.Count == 0) {
                TotalCost = 0;
            }
            TotalCost = _lines.Sum(x => _catalog.GetPrice(x.Product)*x.Quantity);
        }
    }
}