using System;

namespace Domain.Base
{
    public interface IOrder
    {
        OrderStatus OrderStatus { get; }
        double TotalCost { get; }
        DateTime? SubmitDate { get; }

        void AddProduct(Product product, int quantity);
        void RemoveProduct(Product product);
        int GetQuantity(Product product);
        void Submit();
    }
}