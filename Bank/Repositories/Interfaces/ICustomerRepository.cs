using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        int getNumberOfCustomers();

        Customers searchCustomerByID(int id);

        decimal getTotalAmountByID(int id);
    }
}
