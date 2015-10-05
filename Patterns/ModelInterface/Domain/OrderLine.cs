using System;
using Patterns.Common;

namespace Patterns.ModelInterface.Domain
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
        Guid IOrderLineStates.OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        Product IOrderLineStates.Product
        {
            get { return Product; }
            set { Product = value; }
        }

        // ----- Fields
        private DateTime _creationDate;
        private Guid _orderId;

        // ----- Properties
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
        public OrderLine()
        {
        }
        public OrderLine(Guid orderId, Product product, int quantity)
        {
            _orderId = orderId;
            _creationDate = DateTime.Now.RoundToSecond();
            Product = product;
            Quantity = quantity;
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
                   target._creationDate == _creationDate;
        }
        public override int GetHashCode()
        {
            unchecked {
                int hashCode = _creationDate.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Product;
                hashCode = (hashCode*397) ^ Quantity;
                return hashCode;
            }
        }

        #endregion
    }
}