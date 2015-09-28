using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;

namespace Domains.Compromise.Domain
{
    public class Order : IOrder
    {
        private readonly ProductCatalog _catalog = new ProductCatalog();

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
            }

            ReCalculateTotalPrice();
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
            if (Lines.Count == 0) {
                TotalCost = 0;
            }
            TotalCost = Lines.Sum(x => _catalog.GetPrice(x.Product) * x.Quantity);
        }
    }
}