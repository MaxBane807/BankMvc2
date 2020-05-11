using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Extensions
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
