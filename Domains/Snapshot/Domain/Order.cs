﻿using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;
using Domains.Snapshot.Infrastructure;

namespace Domains.Snapshot.Domain
{
    public class Order : IOrder, IStateSnapshotable<OrderState>
    {
        private readonly ProductCatalog _catalog = new ProductCatalog();
        private readonly List<OrderLine> _lines = new List<OrderLine>();

        public Guid Id { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime SubmitDate { get; private set; }
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
                _lines.Add(new OrderLine(product, quantity, Id));
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

        // ----- Snapshot
        OrderState IStateSnapshotable<OrderState>.TakeSnapshot()
        {
            return new OrderState
            {
                Id = Id,
                OrderStatus = OrderStatus,
                SubmitDate = SubmitDate,
                TotalCost = TotalCost,
                Lines = _lines.TakeSnapshot<OrderLine, OrderLineState>().ToList()
            };
        }
        void IStateSnapshotable<OrderState>.LoadFromSnapshot(OrderState orderState)
        {
            Id = orderState.Id;
            OrderStatus = orderState.OrderStatus;
            SubmitDate = orderState.SubmitDate;
            TotalCost = orderState.TotalCost;

            _lines.Clear();
            _lines.LoadFromSnapshot(orderState.Lines);
        }
    }
}