namespace ClassLibrary1
{
    public class PureOrderLineWithNoPersistance : IOrderLine
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
        public PureOrderLineWithNoPersistance(Product product, int quantity)
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