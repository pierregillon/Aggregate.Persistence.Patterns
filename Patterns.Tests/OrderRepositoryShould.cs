using System.Collections.Generic;
using NFluent;
using Patterns.Contract.Domain;
using Xunit;

namespace Patterns.Tests
{
    public class OrderRepository_should
    {
        [Theory]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void persist_new_order<TModel, TRepository>(TModel order, TRepository orderRepository)
            where TModel : IOrder
            where TRepository : IRepository<TModel>
        {
            // Arrange
            order.AddProduct(Product.Shoes, 2);
            order.AddProduct(Product.Tshirt, 1);
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.RemoveProduct(Product.Shoes);
            order.Submit();

            // Acts
            orderRepository.Add(order);
            var loadedOrder = orderRepository.Get(order.Id);

            // Asserts
            Check.That(loadedOrder).IsEqualTo(order);
        }

        [Theory]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void update_existing_order<TModel, TRepository>(TModel order, TRepository orderRepository)
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
        [MemberData(nameof(Binary))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
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
            get {
                return new[] {
                    new object[] { new Compromise.Domain.Order(), new Compromise.Infrastructure.EntityFrameworkOrderRepository() },
                    new object[] { new Compromise.Domain.Order(), new Compromise.Infrastructure.DapperOrderRepository() }
                };
            }
        }
        public static IEnumerable<object[]> EventSourcing
        {
            get {
                return new[] {
                    new object[] { new EventSourcing.Domain.Order(), new EventSourcing.Infrastructure.EntityFrameworkOrderRepository() },
                    new object[] { new EventSourcing.Domain.Order(), new EventSourcing.Infrastructure.DapperOrderRepository() }
                };
            }
        }
        public static IEnumerable<object[]> StateInterface
        {
            get {
                return new[] {
                    new object[] { new StateInterface.Domain.Order(), new StateInterface.Infrastructure.EntityFrameworkOrderRepository(new StateInterface.Infrastructure.Mapping.OrderInterfaceMapper()) },
                    new object[] { new StateInterface.Domain.Order(), new StateInterface.Infrastructure.EntityFrameworkOrderRepository(new StateInterface.Infrastructure.Mapping.OrderAutoMapper()) },
                    new object[] { new StateInterface.Domain.Order(), new StateInterface.Infrastructure.DapperOrderRepository(new StateInterface.Infrastructure.Mapping.OrderInterfaceMapper()) },
                    new object[] { new StateInterface.Domain.Order(), new StateInterface.Infrastructure.DapperOrderRepository(new StateInterface.Infrastructure.Mapping.OrderAutoMapper()) }
                };
            }
        }
        public static IEnumerable<object[]> StateSnapshot
        {
            get {
                return new[] {
                    new object[] { new StateSnapshot.Domain.Order(), new StateSnapshot.Infrastructure.EntityFrameworkOrderRepository() },
                    new object[] { new StateSnapshot.Domain.Order(), new StateSnapshot.Infrastructure.DapperOrderRepository() }
                };
            }
        }
        public static IEnumerable<object[]> InnerClass
        {
            get {
                return new[] {
                    new object[] { new InnerClass.Domain.Order(), new InnerClass.Infrastructure.EntityFrameworkOrderRepository() },
                    new object[] { new InnerClass.Domain.Order(), new InnerClass.Infrastructure.DapperOrderRepository() }
                };
            }
        }

        private static IEnumerable<object[]> GetParameters<TOrder, TRepository>(TOrder order, TRepository repository)
        {
            return new[] {
                new object[] { order, repository }
            };
        }
    }
}