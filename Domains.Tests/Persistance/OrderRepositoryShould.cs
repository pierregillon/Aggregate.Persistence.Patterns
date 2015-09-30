using System.Collections.Generic;
using Domain.Base;
using NFluent;
using Xunit;

namespace Domains.Tests.Persistance
{
    public class OrderRepository_should
    {
        [Theory]
        [MemberData("Binary")]
        [MemberData("Compromise")]
        [MemberData("EventSourcing")]
        [MemberData("ModelInterface")]
        [MemberData("Snapshot")]
        public void persist_order<TModel, TRepository>(TModel order, TRepository orderRepository)
            where TModel : IOrder
            where TRepository : IRepository<TModel>
        {
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 1);
            order.Submit();

            orderRepository.Add(order);
            var loadedOrder = orderRepository.Get(order.Id);

            Check.That(loadedOrder).IsEqualTo(order);
        }

        [Theory]
        [MemberData("Binary2")]
        public void persist_order2<TModel, TRepository>()
            where TModel : IOrder, new()
            where TRepository : IRepository<TModel>, new()
        {
            var order = new TModel();
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 1);
            order.Submit();

            var orderRepository = new TRepository();
            orderRepository.Add(order);
            var loadedOrder = orderRepository.Get(order.Id);

            Check.That(loadedOrder).IsEqualTo(order);
        }

        // ----- Properties

        public static IEnumerable<object[]> Binary
        {
            get { return GetParameters(new Binary.Domain.Order(), new Binary.Infrastructure.OrderRepository()); }
        }
        public static IEnumerable<object[]> Binary2
        {
            get { return GetParameters(typeof(Binary.Domain.Order), typeof(Binary.Infrastructure.OrderRepository)); }
        }
        public static IEnumerable<object[]> Compromise
        {
            get { return GetParameters(new Compromise.Domain.Order(), new Compromise.Infrastructure.OrderRepository()); }
        }
        public static IEnumerable<object[]> EventSourcing
        {
            get { return GetParameters(new EventSourcing.Domain.Order(), new EventSourcing.Infrastructure.OrderRepository()); }
        }
        public static IEnumerable<object[]> ModelInterface
        {
            get { return GetParameters(new ModelInterface.Domain.Order(), new ModelInterface.Infrastructure.OrderRepository()); }
        }
        public static IEnumerable<object[]> Snapshot
        {
            get { return GetParameters(new Snapshot.Domain.Order(), new Snapshot.Infrastructure.OrderRepository()); }
        }
        private static IEnumerable<object[]> GetParameters<TOrder, TRepository>(TOrder order, TRepository repository)
        {
            return new[]
            {
                new object[] {order, repository}
            };
        }
    }
}