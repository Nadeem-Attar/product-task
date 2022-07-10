using ProductsTask.SqlServer.Database;

namespace ProductsTask.Data.Repositories
{
    public class BasicRepository
    {
        public PTDbContext Context { get; }
        public BasicRepository(PTDbContext context)
        {
            Context = context;
        }
    }
}
