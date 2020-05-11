using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Services.Interfaces
{
    public interface ICustomerService
    {
        Customers getCustomerByID(string id);
        public decimal getTotalAmountByID(int id);

        public IQueryable<Customers> getListedCustomers(int pagesize, int currentPage);
        int getNumberOfCustomers();
    }
}
