using System.Data.Entity;

namespace Patterns.Contract.Infrastructure
{
    public abstract class DataContextBase<T> : DbContext where T : DbContext
    {
        protected DataContextBase() : base(SqlConnectionLocator.LocalhostSqlExpress())
        {
            Database.SetInitializer<T>(null);
        }
    }
}