using System;
using Patterns.Contract;
using Patterns.Contract.Domain;

namespace Patterns.Compromise.Domain
{
    public class OrderLine : IOrderLine
    {
        // ----- Properties
        public DateTime CreationDate { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }

        // ----- Constructors
        public OrderLine()
        {
        }
        public OrderLine(Product product, int quantity, Guid id)
        {
            Product = product;
            Quantity = quantity;
            OrderId = id;
            CreationDate = DateTime.Now.RoundToSecond();
        }

        // ----- Public methods
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        #region Overrides with no interest

        public override bool Equals(object obj)
        {
            var target = obj as OrderLine;
            if (target == null) {
                return base.Equals(obj);
            }

            return target.Product == Product &&
                   target.Quantity == Quantity &&
                   target.CreationDate == CreationDate;
        }
        public override int GetHashCode()
        {
            unchecked {
                int hashCode = CreationDate.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Product;
                hashCode = (hashCode*397) ^ Quantity;
                return hashCode;
            }
        }

        #endregion
    }
}