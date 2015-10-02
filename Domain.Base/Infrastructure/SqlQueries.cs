namespace Domain.Base.Infrastructure
{
    public class SqlQueries
    {
        public const string SelectOrdersByIdQuery = "SELECT Id, OrderStatus, TotalCost, SubmitDate " +
                                                    "FROM [dbo].[Order] " +
                                                    "WHERE Id = @id";

        public const string SelectOrderLinesByIdQuery = "SELECT CreationDate, Product, Quantity " +
                                                        "FROM [dbo].[OrderLine] " +
                                                        "WHERE OrderId = @id";

        public const string SelectOrderEventQuery = "SELECT AggregateId, CreationDate, Content, Name " +
                                                    "FROM OrderEvent " +
                                                    "WHERE AggregateId = @id";

        public const string InsertOrderQuery = "INSERT INTO [dbo].[Order] (Id, OrderStatus, TotalCost, SubmitDate) " +
                                               "VALUES(@Id, @OrderStatus, @TotalCost, @SubmitDate)";

        public const string InsertOrderLineQuery = "INSERT INTO [dbo].[OrderLine] (CreationDate, Product, Quantity, OrderId)" +
                                                   " VALUES(@CreationDate, @Product, @Quantity, @OrderId)";

        public const string InsertOrderEventQuery = "INSERT INTO OrderEvent (AggregateId, CreationDate, Content, Name) " +
                                                    "VALUES(@AggregateId, @CreationDate, @Content, @Name)";
    }
}