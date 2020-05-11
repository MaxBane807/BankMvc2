using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.Data;

namespace Bank.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _context;
        public TransactionService(BankAppDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Transactions> GetandOrderTransactionsByAccountID(int id, int nrToGet)
        {
            return _context.Transactions.Where(x => x.TransactionId == id).OrderByDescending(x => x.Date).Take(nrToGet);
        }
    }
}
