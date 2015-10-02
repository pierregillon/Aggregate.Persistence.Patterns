using System;

namespace Patterns.Common
{
    public interface IOrder
    {
        Guid Id { get; }
        double TotalCost { get; }
        DateTime? SubmitDate { get; }

        void AddProduct(Product product, int quantity);
        void RemoveProduct(Product product);
        int GetQuantity(Product product);
        void Submit();
    }
}