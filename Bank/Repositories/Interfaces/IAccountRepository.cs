using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        int getNrOfAccounts();

        decimal getSumOfAccounts();

        List<int> getAccountsByCustomerID(int customerid);

        Accounts getAccountByID(int accountid);
    }
}
