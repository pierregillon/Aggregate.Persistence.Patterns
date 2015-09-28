using System.Collections.Generic;

namespace Domains.ModelInterface.Domain
{
    public static class CopyExtensions
    {
        public static void CopyTo<TSource, TTarget>(this IOrderStates<TSource> source, IOrderStates<TTarget> target)
            where TSource : IOrderLineStates
            where TTarget : IOrderLineStates, new()
        {
            target.Id = source.Id;
            target.OrderStatus = source.OrderStatus;
            target.SubmitDate = source.SubmitDate;
            target.TotalCost = source.TotalCost;
            source.Lines.CopyTo(target.Lines);
        }

        public static void CopyTo(this IOrderLineStates source, IOrderLineStates target)
        {
            target.Product = source.Product;
            target.Quantity = source.Quantity;
        }

        public static void CopyTo<TSource, TTarget>(this IEnumerable<TSource> source, ICollection<TTarget> target)
            where TSource : IOrderLineStates
            where TTarget : IOrderLineStates, new()
        {
            target.Clear();
            foreach (var orderLine in source) {
                var persistantOrderLine = new TTarget();
                orderLine.CopyTo(persistantOrderLine);
                target.Add(persistantOrderLine);
            }
        }
    }
}