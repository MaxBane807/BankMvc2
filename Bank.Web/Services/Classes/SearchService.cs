using Bank.Search;
using Bank.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.Services.Classes
{
    public class SearchService : ISearchService
    {
        private SearchSettings _searchSettings;
        private SearchCustomers _searchCustomers;
        private ManageSearchData _manageSearchData;
        
        public SearchService(IOptions<SearchSettings> searchOptions)
        {
            this._searchSettings = searchOptions.Value;
            this._searchCustomers = new SearchCustomers(_searchSettings.ServiceName, _searchSettings.IndexName, _searchSettings.ApiKey);
            this._manageSearchData = new ManageSearchData(_searchSettings.ServiceName, _searchSettings.IndexName, _searchSettings.ApiKey);
        }
        public CustomerResult GetPagedCustomerIds(string search, string sortField, bool asc, int pageSize, int currentPage)
        {
            return _searchCustomers.GetPagedCustomerIds(search, sortField, asc, pageSize, currentPage);
        }

        public void CreateCustomerData(CustomerIndex customerToCreate)
        {
            _manageSearchData.CreateCustomerData(customerToCreate);
        }

        public void UpdateCustomerData(CustomerIndex customerToUpdate)
        {
            _manageSearchData.UpdateCustomerData(customerToUpdate);
        }

        public void DeleteCustomerData(CustomerIndex customerToDelete)
        {
            _manageSearchData.DeleteCustomerData(customerToDelete);
        }
    }
}
