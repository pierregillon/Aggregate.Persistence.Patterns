namespace Domain.Base
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

        // ----- Overrides
        public override bool Equals(object obj)
        {
            var target = obj as PureOrderLineWithNoPersistance;
            if (target == null) {
                return base.Equals(obj);
            }

            return target.Product == Product &&
                   target.Quantity == Quantity;
        }
        public override int GetHashCode()
        {
            unchecked {
                return ((int) Product*397) ^ Quantity;
            }
        }
    }
}