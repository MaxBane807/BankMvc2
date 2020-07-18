using System.Linq;
using Bank.Web.Models;
using Bank.Web.ServiceModels.AdminServiceModels;

namespace Bank.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Customers getCustomerByUniqueID(string uniqueId);
        decimal getTotalAmountByID(int id);

        IQueryable<Customers> getListedCustomers(int pagesize, int currentPage, string searchName, string searchCity);
        int getNumberOfCustomers();
        int getNumberOfCustomersBySearch(string searchName,string searchCity);
        string CreateCustomer(CreateCustomerServiceModel model);
        Customers GetCustomerByNationalId(string nationalId);
    }
}
