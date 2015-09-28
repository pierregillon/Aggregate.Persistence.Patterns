using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Base;
using NFluent;
using Xunit;

namespace Domains.Tests.Domain
{
    public class Order_should
    {
        public static IEnumerable<object[]> Orders
        {
            get
            {
                return new[]
                {
                    new[] {typeof(Domains.Compromise.Domain.Order)},
                    new[] {typeof(Domains.Snapshot.Domain.Order)},
                    new[] {typeof(Domains.ModelInterface.Domain.Order)},
                };
            }
        }

        [Theory]
        [MemberData("Orders")]
        public void update_quantity_when_adding_existing_product(Type type)
        {
            InvokeMethod(type);
        }

        [Theory]
        [MemberData("Orders")]
        public void update_total_cost_when_adding_products(Type type)
        {
            InvokeMethod(type);
        }

        [Theory]
        [MemberData("Orders")]
        public void update_total_cost_when_removing_product(Type type)
        {
            InvokeMethod(type);
        }

        [Theory]
        [MemberData("Orders")]
        public void throw_exception_when_adding_product_to_submitted_order(Type type)
        {
            InvokeMethod(type);
        }

        [Theory]
        [MemberData("Orders")]
        public void throw_exception_when_removing_product_if_submitted(Type type)
        {
            InvokeMethod(type);
        }

        [Theory]
        [MemberData("Orders")]
        public void throw_exception_when_trying_to_submit_twice(Type type)
        {
            InvokeMethod(type);
        }

        // ----- Test body
        
        public void update_quantity_when_adding_existing_product<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Jacket, 3);

            Check.That(order.GetQuantity(Product.Jacket)).IsEqualTo(4);
        }
        public void update_total_cost_when_adding_products<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.AddProduct(Product.Jacket, 1);
            order.AddProduct(Product.Computer, 1);
            order.AddProduct(Product.Tshirt, 2);
            order.AddProduct(Product.Shoes, 2);

            Check.That(order.TotalCost).IsEqualTo(1134.9);
        }
        public void update_total_cost_when_removing_product<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.AddProduct(Product.Jacket, 1);
            order.RemoveProduct(Product.Jacket);

            Check.That(order.TotalCost).IsEqualTo(0);
        }
        public void throw_exception_when_adding_product_to_submitted_order<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.Submit();

            Check.ThatCode(() => order.AddProduct(Product.Computer, 1)).Throws<OrderOperationException>();
        }
        public void throw_exception_when_removing_product_if_submitted<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.AddProduct(Product.Computer, 1);
            order.Submit();

            Check.ThatCode(() => order.RemoveProduct(Product.Computer)).Throws<OrderOperationException>();
        }
        public void throw_exception_when_trying_to_submit_twice<TOrder>() where TOrder : IOrder, new()
        {
            var order = new TOrder();

            order.Submit();

            Check.ThatCode(order.Submit).Throws<OrderOperationException>();
        }

        // ----- Internal method

        private void InvokeMethod(Type type, [CallerMemberName] string memberName = null)
        {
            this.GetType()
                .GetMethods()
                .Single(x => x.Name == memberName && x.IsGenericMethod)
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { });
        }
    }
}