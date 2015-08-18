using System;
using ClassLibrary1;
using Domains.Snapshot.Infrastructure;

namespace Domains.Snapshot.Domain
{
    public class OrderLine : IOrderLine, IStateSnapshotable<OrderLineState>
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        // ----- Constructor
        public OrderLine()
        {
        }
        public OrderLine(Product product, int quantity, Guid id)
        {
            Product = product;
            Quantity = quantity;
            OrderId = id;
        }

        // ----- Public methods
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        // ----- Snapshot
        public OrderLineState TakeSnapshot()
        {
            return new OrderLineState
            {
                Product = Product,
                Quantity = Quantity,
                OrderId = OrderId
            };
        }
        public void LoadFromSnapshot(OrderLineState orderState)
        {
            Product = orderState.Product;
            Quantity = orderState.Quantity;
            OrderId = orderState.OrderId;
        }
    }
}