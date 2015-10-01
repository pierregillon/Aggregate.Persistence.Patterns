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
        Product IOrderLineStates.Product
        {
            get { return Product; }
            set { Product = value; }
        }

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
        }

        // ----- Public methods
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}