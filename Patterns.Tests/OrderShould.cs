using System;
using System.Collections.Generic;
using Aggregate.Persistence.NoPersistence.Domain;
using Common.Domain;
using NFluent;
using Xunit;

namespace Patterns.Tests
{
    public class Order_should
    {
        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void increase_quantity_when_adding_existing_product<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Jacket, 3);

            Check.That(order.GetQuantity(Product.Jacket)).IsEqualTo(4);
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void calculate_total_cost_when_adding_products<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.AddProduct(Product.Shoes, 2);

            Check.That(order.TotalCost).IsEqualTo(1134.9);
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void calculate_total_cost_when_removing_product<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.AddProduct(Product.Jacket, 1);
            order.RemoveProduct(Product.Jacket);

            Check.That(order.TotalCost).IsEqualTo(0);
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void throw_exception_when_adding_product_with_negative_quantity<TOrder>(TOrder order) where TOrder : IOrder
        {
            Check
                .ThatCode(() => order.AddProduct(Product.Computer, -5))
                .Throws<OrderOperationException>()
                .WithMessage("Unable to add product with negative quantity.");
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void throw_exception_when_adding_product_with_no_quantity<TOrder>(TOrder order) where TOrder : IOrder
        {
            Check
                .ThatCode(() => order.AddProduct(Product.Computer, 0))
                .Throws<OrderOperationException>()
                .WithMessage("Unable to add product with no quantity.");
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void throw_exception_when_adding_product_to_submitted_order<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.Submit();

            Action action = () => order.AddProduct(Product.Computer, 1);

            Check.ThatCode(action).Throws<OrderOperationException>();
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void throw_exception_when_removing_product_if_submitted<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.AddProduct(Product.Computer, 1);
            order.Submit();

            Action action = () => order.RemoveProduct(Product.Computer);

            Check.ThatCode(action).Throws<OrderOperationException>();
        }

        [Theory]
        [MemberData(nameof(NoPersistence))]
        [MemberData(nameof(Binary))]
        [MemberData(nameof(Compromise))]
        [MemberData(nameof(EventSourcing))]
        [MemberData(nameof(StateInterface))]
        [MemberData(nameof(StateSnapshot))]
        [MemberData(nameof(InnerClass))]
        public void throw_exception_when_trying_to_submit_twice<TOrder>(TOrder order) where TOrder : IOrder
        {
            order.Submit();

            Check.ThatCode(order.Submit).Throws<OrderOperationException>();
        }

        // ----- Properties
        public static IEnumerable<object[]> NoPersistence => GetParameters(new Order());
        public static IEnumerable<object[]> Binary => GetParameters(new Aggregate.Persistence.Binary.Domain.Order());
        public static IEnumerable<object[]> Compromise => GetParameters(new Aggregate.Persistence.Compromise.Domain.Order());
        public static IEnumerable<object[]> EventSourcing => GetParameters(new Aggregate.Persistence.EventSourcing.Domain.Order());
        public static IEnumerable<object[]> StateInterface => GetParameters(new Aggregate.Persistence.StateInterface.Domain.Order());
        public static IEnumerable<object[]> StateSnapshot => GetParameters(new Aggregate.Persistence.StateSnapshot.Domain.Order());
        public static IEnumerable<object[]> InnerClass => GetParameters(new Aggregate.Persistence.InnerClass.Domain.Order());

        private static IEnumerable<object[]> GetParameters<TOrder>(TOrder order)
        {
            return new[] {
                new object[] { order }
            };
        }
    }
}