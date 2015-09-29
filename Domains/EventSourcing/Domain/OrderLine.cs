using Domain.Base;

namespace Domains.EventSourcing.Domain
{
    public class OrderLine : IOrderLine
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
    }
}