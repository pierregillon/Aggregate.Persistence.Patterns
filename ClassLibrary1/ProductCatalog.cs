using System.Collections.Generic;

namespace ClassLibrary1
{
    public class ProductCatalog
    {
        private readonly Dictionary<Product, double> _prices = new Dictionary<Product, double>
        {
            {Product.Tshirt, 3.00},
            {Product.Jacket, 200.50},
            {Product.Computer, 688.00},
            {Product.Shoes, 120.20},
        };

        public double GetPrice(Product product)
        {
            double price;
            if (_prices.TryGetValue(product, out price)) {
                return price;
            }
            return 0;
        }
    }
}