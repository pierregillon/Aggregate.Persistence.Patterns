using System.Data.SqlClient;

namespace Domain.Base.Infrastructure
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