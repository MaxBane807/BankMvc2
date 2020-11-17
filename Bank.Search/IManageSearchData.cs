using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Search
{
    public interface IManageSearchData
    {
        void CreateCustomerData(CustomerIndex customerToCreate);
        void UpdateCustomerData(CustomerIndex customerToUpdate);
        void DeleteCustomerData(CustomerIndex customerToDelete);
    }
}
