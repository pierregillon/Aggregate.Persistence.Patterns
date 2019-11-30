using System.Collections.Generic;

namespace Patterns.Contract.Domain
{
    public class PriceCatalog
    {
        private static PriceCatalog _instance;
        public static PriceCatalog Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = new PriceCatalog();
                }
                return _instance;
            }
        }

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