using System;
using Patterns.Common;
using Patterns.Common.Domain;

namespace Patterns.StateSnapshot.Domain
{
    public class OrderLine : IOrderLine, IStateSnapshotable<OrderLineState>
    {
        // ----- Fields
        private DateTime _creationDate;

        // ----- Properties
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
            _creationDate = DateTime.Now.RoundToSecond();
        }

        // ----- Public methods
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        // ----- State Snapshot
        OrderLineState IStateSnapshotable<OrderLineState>.TakeSnapshot()
        {
            return new OrderLineState
            {
                Product = Product,
                Quantity = Quantity,
                CreationDate = _creationDate
            };
        }
        void IStateSnapshotable<OrderLineState>.LoadSnapshot(OrderLineState snapshot)
        {
            Product = snapshot.Product;
            Quantity = snapshot.Quantity;
            _creationDate = snapshot.CreationDate;
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