using System;
using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public class OrderLine : IOrderLine, IOrderLineStates
    {
        int IOrderLineStates.Quantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }
        DateTime IOrderLineStates.CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        Product IOrderLineStates.Product
        {
            get { return Product; }
            set { Product = value; }
        }

        private DateTime _creationDate;

        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
        public OrderLine()
        {
        }
        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            _creationDate = DateTime.Now;
        }

        // ----- Public methods
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
                    target.Quantity == Quantity &&
                    target._creationDate == _creationDate;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _creationDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Product;
                hashCode = (hashCode * 397) ^ Quantity;
                return hashCode;
            }
        }
    }
}