using System.Linq;

namespace ProductsTask.SharedKernal.Extension
{
    public static class ExtensionMethods
    {
        public static IQueryable<TEntity> GetDataWithPagination<TEntity>(this IQueryable<TEntity> data,
           int pageSize = 8, int pageNumber = 0)
        {
            return data.Skip(pageSize * pageNumber).Take(pageSize);
        }
    }
}
