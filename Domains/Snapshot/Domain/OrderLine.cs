using ClassLibrary1;
using Domains.Snapshot.Infrastructure;

namespace Domains.Snapshot.Domain
{
    public class OrderLine : IOrderLine, IStateSnapshotable<OrderLineState>
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        // ----- Constructor
        public OrderLine()
        {
        }
        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
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
            };
        }
        public void LoadFromSnapshot(OrderLineState orderState)
        {
            Product = orderState.Product;
            Quantity = orderState.Quantity;
        }
    }
}