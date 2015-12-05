using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Common;
using Patterns.Common.Domain;

namespace Patterns.Compromise.Domain
{
    public class Order : IOrder
    {
        private readonly PriceCatalog _catalog = new PriceCatalog();

        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double TotalCost { get; set; }
        public List<OrderLine> Lines { get; set; }

        // ----- Constructor
        public Order()
        {
            Id = Guid.NewGuid();
            Lines = new List<OrderLine>();
        }

        // ----- Public methods
        public void AddProduct(Product product, int quantity)
        {
            CheckIfDraft();
            CheckQuantity(quantity);

            var line = Lines.FirstOrDefault(x => x.Product == product);
            if (line == null) {
                Lines.Add(new OrderLine(product, quantity, Id));
            }
            else {
                line.IncreaseQuantity(quantity);
            }

            ReCalculateTotalPrice();
        }
        public void RemoveProduct(Product product)
        {
            CheckIfDraft();

            var line = Lines.FirstOrDefault(x => x.Product == product);
            if (line != null) {
                Lines.Remove(line);
                ReCalculateTotalPrice();
            }
        }
        public int GetQuantity(Product product)
        {
            var line = Lines.FirstOrDefault(x => x.Product == product);
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
            if (Lines.Count == 0) {
                TotalCost = 0;
            }
            TotalCost = Lines.Sum(x => _catalog.GetPrice(x.Product)*x.Quantity);
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
                   target.Lines.IsEquivalentIgnoringOrderTo(Lines);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return "Order with compromise pattern";
        }

        #endregion
    }
}