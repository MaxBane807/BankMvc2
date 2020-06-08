using System.Linq;
using Bank.Web.Models;

namespace Bank.Web.Services.Interfaces
{
    public interface ICustomerService
    {
        Customers getCustomerByID(string id);
        public decimal getTotalAmountByID(int id);

        public IQueryable<Customers> getListedCustomers(int pagesize, int currentPage, string searchName, string searchCity);
        int getNumberOfCustomers();
        int getNumberOfCustomersBySearch(string searchName,string searchCity);
    }
}
