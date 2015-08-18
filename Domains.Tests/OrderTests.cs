using ClassLibrary1;
using Domains.Compromise.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Domains.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void AssureOrderInCompromisePatternIsValid()
        {
            CannotAddProductIfSubmitted<Order>();
            UpdateQuantityIfProductAlreadyExist<Order>();
            CannotRemoveProductIfSubmitted<Order>();
            CannotSubmitOrderIfAlreadySubmitted<Order>();
            CalculateTotalCostEveryProductAdd<Order>();
            CalculateTotalCostEveryProductRemove<Order>();
        }

        // ----- Test bases
        private static void CannotAddProductIfSubmitted<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.Submit();
            Check.ThatCode(() => order.AddProduct(Product.Computer, 1)).Throws<OrderOperationException>();
        }
        private static void UpdateQuantityIfProductAlreadyExist<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Jacket, 3);
            Check.That(order.GetQuantity(Product.Jacket)).IsEqualTo(4);
        }
        private static void CalculateTotalCostEveryProductAdd<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.AddProduct(Product.Shoes, 2);
            Check.That(order.TotalCost).IsEqualTo(1134.9);
        }
        private static void CalculateTotalCostEveryProductRemove<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.AddProduct(Product.Jacket, 1);
            order.RemoveProduct(Product.Jacket);
            Check.That(order.TotalCost).IsEqualTo(0);
        }
        private static void CannotRemoveProductIfSubmitted<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.AddProduct(Product.Computer, 1);
            order.Submit();
            Check.ThatCode(() => order.RemoveProduct(Product.Computer)).Throws<OrderOperationException>();
        }
        private static void CannotSubmitOrderIfAlreadySubmitted<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();
            order.Submit();
            Check.ThatCode(order.Submit).Throws<OrderOperationException>();
        }
    }
}
