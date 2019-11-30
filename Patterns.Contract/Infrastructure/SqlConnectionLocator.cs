namespace Patterns.Contract.Infrastructure
{
    public class SqlConnectionLocator
    {
        public static string LocalhostSqlExpress()
        {
            return @"Data Source=localhost\SQLEXPRESS;Database=Patterns.Tables;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
        }
    }
}