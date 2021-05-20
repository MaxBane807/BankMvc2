using Bank.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Services.Interfaces
{
    public interface ISearchService
    {
        CustomerResult GetPagedCustomerIds(string search, string sortField, bool asc, int pageSize, int currentPage);

        public void CreateCustomerData(CustomerIndex customerToCreate);

        public void UpdateCustomerData(CustomerIndex customerToUpdate);

        public void DeleteCustomerData(CustomerIndex customerToDelete);
    }
}
