using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.ViewModels;

namespace Bank.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        int getNumberOfCustomers();

       

        IQueryable<> getTotalAmountByID(int id);

        IQueryable<ListCustomersViewModel.CustomerViewModel>getListedCustomers(int pagesize, int currentPage);
    }
}
