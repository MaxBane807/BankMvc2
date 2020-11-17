using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Search
{
    public interface ISearchCustomers
    {
        CustomerResult GetPagedCustomerIds(string search, string sortField, bool asc, int pageSize, int currentPage);
    }
}
