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
        IEnumerable<OrderLine> IOrderStates<OrderLine>.Lines
        {
            get
            {
                // We do a copy of the origin list to avoid
                // modification from outside.
                return _lines.ToArray();
            }
            set { _lines = value.ToList(); }
        }

        private readonly ProductCatalog _catalog = new ProductCatalog();
        private List<OrderLine> _lines = new List<OrderLine>();
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
            CheckQuantity(quantity);

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
                ReCalculateTotalPrice();
            }
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
        private void CheckQuantity(int quantity)
        {
            if (quantity < 0) {
                throw new OrderOperationException("Unable to add product with negative quantity.");
            }
            if (quantity == 0) {
                throw new OrderOperationException("Unable to add product with no quantity.");
            }
        }
        private void ReCalculateTotalPrice()
        {
            if (_lines.Count == 0) {
                TotalCost = 0;
            }
            TotalCost = _lines.Sum(x => _catalog.GetPrice(x.Product)*x.Quantity);
        }

        // ----- Overrides
        public override bool Equals(object obj)
        {
            var target = obj as Order;
            if (target == null) {
                return base.Equals(obj);
            }

            return target.Id == Id &&
                   target._orderStatus == _orderStatus &&
                   target.SubmitDate == SubmitDate &&
                   target.TotalCost == TotalCost &&
                   target._lines.IsEquivalentIgnoringOrderTo(_lines);
        }
        protected bool Equals(Order other)
        {
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return "Order with model interface pattern";
        }
    }
}