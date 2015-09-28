namespace Domains.ModelInterface.Domain
{
    public static class OrderConverter
    {
        public static void CopyTo(this IOrderPersistantModel source, IOrderPersistantModel target)
        {
            target.Id = source.Id;
            target.OrderStatus = source.OrderStatus;
            target.SubmitDate = source.SubmitDate;
            target.TotalCost = source.TotalCost;
            target.Lines = source.Lines;
        }

        public static void CopyTo(this IOrderLinePersistantModel source, IOrderLinePersistantModel target)
        {
            target.Product = source.Product;
            target.Quantity = source.Quantity;
        }
    }
}