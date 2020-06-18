using System.Collections.Generic;
using Bank.Web.Models;

namespace Bank.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Accounts PrepareViewAccount(int id);
        List<int> getAccountsByCustomerID(string customerid);

        int GetNrOfAccounts();
        decimal GetSumOfAccounts();

        decimal GetAccountBalanceByID(int id);

        bool CheckIfAccountExists(int id);
    }
}
