using System.Collections.Generic;
using Bank.Web.Models;

namespace Bank.Web.Services.Interfaces
{
    
    public interface ITransactionService
    {
        IEnumerable<Transactions> GetandOrderTransactionsByAccountID(int id,int nrToGet, int nrToSkip);

        int CountTransactionsByAccountID(int accountID);
    }
}
