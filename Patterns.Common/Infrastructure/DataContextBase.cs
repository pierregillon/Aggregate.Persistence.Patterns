using System.Data.Entity;

namespace Patterns.Common.Infrastructure
{
    public abstract class DataContextBase : DbContext
    {
        protected DataContextBase()
            : base(SqlConnectionLocator.LocalhostSqlExpress())
        {
            
        }
    }
}