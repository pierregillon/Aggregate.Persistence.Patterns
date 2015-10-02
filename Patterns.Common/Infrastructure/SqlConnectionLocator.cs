using System.Data.SqlClient;

namespace Patterns.Common.Infrastructure
{
    public class SqlConnectionLocator
    {
        public static string LocalhostSqlExpress()
        {
            var builder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "DomainModelPersistencePatterns", 
                DataSource = "localhost\\SQLEXPRESS", 
                IntegratedSecurity = true
            };
            return builder.ConnectionString;
        }
    }
}