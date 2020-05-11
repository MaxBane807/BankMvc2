using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void CreateInsertTransaction(int AccountID,string operation,int amount,string symbol,string bank,string otheraccount);
        public IEnumerable<Transactions> GetTransactionsByAccountID(int id);
    }
}
