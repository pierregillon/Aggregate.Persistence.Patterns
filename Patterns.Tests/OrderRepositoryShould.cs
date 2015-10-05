using System.Collections.Generic;
using NFluent;
using Patterns.Common;
using Xunit;

namespace Patterns.Tests
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
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.RemoveProduct(Product.Shoes);
            order.Submit();

            orderRepository.Add(order);
            var loadedOrder = orderRepository.Get(order.Id);

            Check.That(loadedOrder).IsEqualTo(order);
        }

        [Theory]
        [MemberData("Binary")]
        [MemberData("EventSourcing")]
        [MemberData("Compromise")]
        [MemberData("ModelInterface")]
        [MemberData("Snapshot")]
        public void update_order<TModel, TRepository>(TModel order, TRepository orderRepository)
            where TModel : IOrder
            where TRepository : IRepository<TModel>
        {
            // Arrange
            orderRepository.Add(order);

            // Acts
            order = orderRepository.Get(order.Id);
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 1);
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.RemoveProduct(Product.Shoes);
            order.Submit();
            orderRepository.Update(order);

            // Asserts
            var loadedOrder = orderRepository.Get(order.Id);
            Check.That(loadedOrder).IsEqualTo(order);
        }

        [Theory]
        [MemberData("Binary")]
        [MemberData("EventSourcing")]
        [MemberData("Compromise")]
        [MemberData("ModelInterface")]
        public void delete_existing_order<TModel, TRepository>(TModel order, TRepository orderRepository)
            where TModel : class, IOrder
            where TRepository : IRepository<TModel>
        {
            // Arrange
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 2);
            order.Submit();
            orderRepository.Add(order);

            // Acts
            orderRepository.Delete(order.Id);

            // Asserts
            var loadedOrder = orderRepository.Get(order.Id);
            Check.That(loadedOrder).IsNull();
        }

        // ----- Properties

        public static IEnumerable<object[]> Binary
        {
            get { return GetParameters(new Binary.Domain.Order(), new Binary.Infrastructure.OrderRepository()); }
        }
        public static IEnumerable<object[]> Compromise
        {
            get
            {
                return new[]
                {
                    new object[] {new Compromise.Domain.Order(), new Compromise.Infrastructure.EntityFrameworkOrderRepository()},
                    new object[] {new Compromise.Domain.Order(), new Compromise.Infrastructure.DapperOrderRepository()}
                };
            }
        }
        public static IEnumerable<object[]> EventSourcing
        {
            get
            {
                return new[]
                {
                    new object[] {new EventSourcing.Domain.Order(), new EventSourcing.Infrastructure.EntityFrameworkOrderRepository()},
                    new object[] {new EventSourcing.Domain.Order(), new EventSourcing.Infrastructure.DapperOrderRepository()}
                };
            }
        }
        public static IEnumerable<object[]> ModelInterface
        {
            get
            {
                return new[]
                {
                    new object[] {new ModelInterface.Domain.Order(), new ModelInterface.Infrastructure.EntityFrameworkOrderRepository()},
                    new object[] {new ModelInterface.Domain.Order(), new ModelInterface.Infrastructure.DapperOrderRepository()}
                };
            }
        }
        public static IEnumerable<object[]> Snapshot
        {
            get
            {
                return new[]
                {
                    new object[] {new Snapshot.Domain.Order(), new Snapshot.Infrastructure.EntityFrameworkOrderRepository()},
                    new object[] {new Snapshot.Domain.Order(), new Snapshot.Infrastructure.DapperOrderRepository()}
                };
            }
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