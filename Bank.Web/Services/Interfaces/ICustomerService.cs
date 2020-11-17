using System.Collections.Generic;
using System.Linq;
using Bank.Data.Models;
using Bank.Web.ServiceModels.CustomerServiceModels;

namespace Bank.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Customers getCustomerByUniqueID(string uniqueId);
        decimal getTotalAmountByID(int id);

        List<Customers> getListedCustomers(List<int> customerIds);
        int getNumberOfCustomers();
        int getNumberOfCustomersBySearch(string searchName,string searchCity);
        string CreateCustomer(CreateCustomerServiceModel model);
        void ChangeCustomer(ChangeCustomerServiceModel model);
        Customers GetCustomerByNationalId(string nationalId);
    }
}
