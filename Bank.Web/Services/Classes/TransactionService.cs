using System.Collections.Generic;
using System.Linq;
using Bank.Web.Data;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;

namespace Bank.Web.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _context;
        public TransactionService(BankAppDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Transactions> GetandOrderTransactionsByAccountID(int id, int nrToGet,int nrToSkip)
        {
            return _context.Transactions.Where(x => x.AccountId == id).OrderByDescending(x => x.Date).ThenByDescending(x => x.TransactionId).Skip(nrToSkip).Take(nrToGet);
        }

        public int CountTransactionsByAccountID(int accountID)
        {
            return _context.Transactions.Where(x => x.AccountId == accountID).Count();
        }
    }
}
