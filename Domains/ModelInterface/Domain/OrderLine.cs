using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public class OrderLine : IOrderLine, IOrderLinePersistantModel
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