using System.Linq;

namespace Bank.Web.Extensions
{
    public static class QueryObjects
    {
        
        public static IQueryable<int> MyOrder
            (this IQueryable<int> queryable, bool ascending)
        {
            return ascending
                ? queryable.OrderBy(num => num)
                : queryable.OrderByDescending(num => num);
        }
        
    }
}
