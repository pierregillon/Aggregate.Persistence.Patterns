using System;

namespace ClassLibrary1
{
    public class PureOrderLineWithNoPersistance : IOrderLine
    {
        public Guid OrderId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
        public PureOrderLineWithNoPersistance(Product product, int quantity, Guid id)
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
    }
}