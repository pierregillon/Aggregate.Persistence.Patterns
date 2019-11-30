using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Contract;
using Patterns.Contract.Domain;

namespace Patterns.Binary.Domain
{
    [Serializable]
    public class Order : IOrder
    {
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
            SubmitDate = DateTime.Now.RoundToSecond();
            OrderStatus = OrderStatus.Submitted;
        }

        // ----- Internal logic
        private void CheckIfDraft()
        {
            if (OrderStatus != OrderStatus.Draft)
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
            TotalCost = _lines.Sum(x => PriceCatalog.Instance.GetPrice(x.Product)*x.Quantity);
        }

        #region Overrides with no interest

        public override bool Equals(object obj)
        {
            var target = obj as Order;
            if (target == null) {
                return base.Equals(obj);
            }

            return target.Id == Id &&
                   target.OrderStatus == OrderStatus &&
                   target.SubmitDate == SubmitDate &&
                   target.TotalCost == TotalCost &&
                   target._lines.IsEquivalentIgnoringOrderTo(_lines);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return "Order with binary persistence";
        }

        #endregion
    }
}