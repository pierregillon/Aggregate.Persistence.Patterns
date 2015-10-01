using System;
using Domain.Base;

namespace Domains.Snapshot.Domain
{
    public class OrderLine : IOrderLine, IStateSnapshotable<OrderLineState>
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        private DateTime _creationDate;

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

        // ----- Snapshot
        OrderLineState IStateSnapshotable<OrderLineState>.TakeSnapshot()
        {
            return new OrderLineState
            {
                Product = Product,
                Quantity = Quantity,
                CreationDate = _creationDate
            };
        }
        void IStateSnapshotable<OrderLineState>.LoadFromSnapshot(OrderLineState orderState)
        {
            Product = orderState.Product;
            Quantity = orderState.Quantity;
            _creationDate = orderState.CreationDate;
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
            unchecked {
                int hashCode = _creationDate.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Product;
                hashCode = (hashCode*397) ^ Quantity;
                return hashCode;
            }
        }
    }
}