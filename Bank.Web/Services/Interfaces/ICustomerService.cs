using System.Linq;
using Bank.Web.Models;
using Bank.Web.ServiceModels.AdminServiceModels;

namespace Bank.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Customers getCustomerByID(string id);
        decimal getTotalAmountByID(string id);

        IQueryable<Customers> getListedCustomers(int pagesize, int currentPage, string searchName, string searchCity);
        int getNumberOfCustomers();
        int getNumberOfCustomersBySearch(string searchName,string searchCity);
        void CreateCustomer(CreateCustomerServiceModel model);
        Customers GetCustomerByNationalId(string nationalId);
    }
}
