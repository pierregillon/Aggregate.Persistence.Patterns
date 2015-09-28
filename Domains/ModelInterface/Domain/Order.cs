using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public class Order : IOrder, IOrderStates<OrderLine>
    {
        Guid IOrderStates<OrderLine>.Id
        {
            get { return Id; }
            set { Id = value; }
        }
        OrderStatus IOrderStates<OrderLine>.OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }
        DateTime? IOrderStates<OrderLine>.SubmitDate
        {
            get { return SubmitDate; }
            set { SubmitDate = value; }
        }
        double IOrderStates<OrderLine>.TotalCost
        {
            get { return TotalCost; }
            set { TotalCost = value; }
        }
        ICollection<OrderLine> IOrderStates<OrderLine>.Lines
        {
            get { return _lines; }
            set { value.CopyTo(_lines); }
        }

        private readonly ProductCatalog _catalog = new ProductCatalog();
        private readonly List<OrderLine> _lines = new List<OrderLine>();
        private OrderStatus _orderStatus;

        public Guid Id { get; private set; }
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
            _orderStatus = OrderStatus.Submitted;
        }

        // ----- Internal logic
        private void CheckIfDraft()
        {
            if (_orderStatus != OrderStatus.Draft)
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