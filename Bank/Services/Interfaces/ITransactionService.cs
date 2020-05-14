using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    
    public interface ITransactionService
    {
        IEnumerable<Transactions> GetandOrderTransactionsByAccountID(int id,int nrToGet, int nrToSkip);

        int CountTransactionsByAccountID(int accountID);
    }
}
