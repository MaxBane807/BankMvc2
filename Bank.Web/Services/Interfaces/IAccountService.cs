using System.Collections.Generic;
using Bank.Data.Models;

namespace Bank.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Accounts PrepareViewAccount(int id);
        List<int> getAccountsByCustomerID(int customerid);

        int GetNrOfAccounts();
        decimal GetSumOfAccounts();

        decimal GetAccountBalanceByID(int id);

        bool CheckIfAccountExists(int id);
    }
}
