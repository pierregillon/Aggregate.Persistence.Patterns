using System;
using Domain.Base;

namespace Domains.Binary.Domain
{
    [Serializable]
    public class OrderLine : IOrderLine
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        // ----- Constructor
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