using System;
using Domain.Base;

namespace Domains.Compromise.Domain
{
    public class OrderLine : IOrderLine
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderLine()
        {
        }
        public OrderLine(Product product, int quantity, Guid id)
        {
            Product = product;
            Quantity = quantity;
            OrderId = id;
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        // ----- Overrides
        public override bool Equals(object obj)
        {
            var target = obj as OrderLine;
            if (target == null) {
                return base.Equals(obj);
            }

            return target.Product == Product &&
                   target.Quantity == Quantity;
        }
        public override int GetHashCode()
        {
            unchecked {
                return ((int) Product*397) ^ Quantity;
            }
        }
    }
}