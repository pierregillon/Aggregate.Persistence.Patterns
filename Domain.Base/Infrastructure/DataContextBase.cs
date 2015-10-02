using System.Data.Entity;

namespace Domain.Base.Infrastructure
{
    public abstract class DataContextBase : DbContext
    {
        protected DataContextBase()
            : base(SqlConnectionLocator.LocalhostSqlExpress())
        {
            
        }
    }
}