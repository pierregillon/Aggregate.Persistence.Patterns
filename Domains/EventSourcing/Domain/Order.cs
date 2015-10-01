using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Domains.EventSourcing.Domain.Events;

namespace Domains.EventSourcing.Domain
{
    public class Order : EventOwner, IOrder
    {
        private readonly ProductCatalog _catalog = new ProductCatalog();
        private readonly List<OrderLine> _lines = new List<OrderLine>();
        private OrderStatus _orderStatus;

        public Guid Id { get; private set; }
        public DateTime? SubmitDate { get; private set; }
        public double TotalCost { get; private set; }

        // ----- Constructor
        public Order()
        {
            RegisterEvent<OrderCreated>(ApplyOrderCreated);
            RegisterEvent<ProductAdded>(ApplyProductAdded);
            RegisterEvent<ProductRemoved>(ApplyProductRemoved);
            RegisterEvent<OrderSubmitted>(ApplyOrderSubmitted);

            Apply(new OrderCreated(Guid.NewGuid()));
        }

        // ----- Public methods
        public void AddProduct(Product product, int quantity)
        {
            CheckIfDraft();
            CheckQuantity(quantity);

            Apply(new ProductAdded(Id, product, quantity));
        }
        public void RemoveProduct(Product product)
        {
            CheckIfDraft();
            Apply(new ProductRemoved(Id, product));
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
            Apply(new OrderSubmitted(Id, DateTime.Now));
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

        // ----- Callback events
        private void ApplyOrderCreated(OrderCreated @event)
        {
            Id = @event.AggregateId;
        }
        private void ApplyProductAdded(ProductAdded @event)
        {
            var line = _lines.FirstOrDefault(x => x.Product == @event.Product);
            if (line == null) {
                _lines.Add(new OrderLine(@event.Product, @event.Quantity));
            }
            else {
                line.IncreaseQuantity(@event.Quantity);
            }
            ReCalculateTotalPrice();
        }
        private void ApplyProductRemoved(ProductRemoved @event)
        {
            var line = _lines.FirstOrDefault(x => x.Product == @event.Product);
            if (line != null) {
                _lines.Remove(line);
            }
            ReCalculateTotalPrice();
        }
        private void ApplyOrderSubmitted(OrderSubmitted @event)
        {
            _orderStatus = OrderStatus.Submitted;
            SubmitDate = @event.SubmitDate;
        }

        // ----- Overrides
        public override bool Equals(object obj)
        {
            var target = obj as Order;
            if (target == null) {
                return base.Equals(obj);
            }
            return target.Id == Id;
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
            return "Order with eventsourcing pattern";
        }
    }
}