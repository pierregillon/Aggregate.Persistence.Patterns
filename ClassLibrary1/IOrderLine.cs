namespace ClassLibrary1
{
    public interface IOrderLine
    {
        Product Product { get; }
        int Quantity { get; }

        void IncreaseQuantity(int quantity);
    }
}