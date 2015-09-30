using Domain.Base;
using Domains.Binary.Domain;
using Domains.Binary.Infrastructure;
using NFluent;
using Xunit;

namespace Domains.Tests.Persistance
{
    public class OrderRepository_should
    {
        [Fact(DisplayName = "An order repository should persist order")]
        public void persist_order()
        {
            var order = new Order();
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 1);
            order.Submit();

            var orderRepository = new OrderRepository();
            orderRepository.Add(order);
            var loadedOrder = orderRepository.Get(order.Id);

            Check.That(loadedOrder).IsEqualTo(order);
        }
    }
}
