using System.Data.Entity;

namespace Patterns.Common.Infrastructure
{
    public abstract class DataContextBase : DbContext
    {
        protected DataContextBase()
            : base(SqlConnectionLocator.LocalhostSqlExpress())
        {
            DisableDatabaseGeneration();
        }

        private void DisableDatabaseGeneration()
        {
            typeof (Database)
                .GetMethod("SetInitializer")
                .MakeGenericMethod(GetType())
                .Invoke(this, new object[] {null});
        }
    }
}