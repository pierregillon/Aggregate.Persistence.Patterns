namespace Patterns.Contract.Infrastructure
{
    public class SqlConnectionLocator
    {
        public static string LocalhostSqlExpress()
        {
            return "data source=.\\SQLEXPRESS;database = YourDatabaseName";
        }
    }
}
