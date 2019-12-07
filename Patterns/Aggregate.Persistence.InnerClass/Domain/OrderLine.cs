using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Contract;
using Patterns.Contract.Domain;

namespace Patterns.InnerClass.Domain
{
    public class OrderLine : IOrderLine
    {
        // ----- Fields
        private DateTime _creationDate;

        // ----- Properties
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
        public OrderLine() { }

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
                hashCode = (hashCode * 397) ^ (int) Product;
                hashCode = (hashCode * 397) ^ Quantity;
                return hashCode;
            }
        }

        #endregion

        public class FromState
        {
            public OrderLine Build(OrderLineState orderLineState)
            {
                return new OrderLine {
                    Product = orderLineState.Product,
                    Quantity = orderLineState.Quantity,
                    _creationDate = orderLineState.CreationDate
                };
            }

            public IEnumerable<OrderLine> Build(IEnumerable<OrderLineState> lines)
            {
                return lines.Select(Build);
            }
        }

        public class ToState
        {
            public OrderLineState Build(OrderLine orderLine)
            {
                return new OrderLineState {
                    Product = orderLine.Product,
                    Quantity = orderLine.Quantity,
                    CreationDate = orderLine._creationDate
                };
            }

            public IEnumerable<OrderLineState> Build(IEnumerable<OrderLine> lines, Action<OrderLineState> action)
            {
                foreach (var orderLine in lines) {
                    var orderLineState = Build(orderLine);
                    action(orderLineState);
                    yield return orderLineState;
                }
            }
        }
    }
}