using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Bank.Data;
using Bank.Models;
using Bank.Repositories.Interfaces;

namespace Bank.Repositories.Classes
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankAppDataContext _context;
        public TransactionRepository(BankAppDataContext context)
        {
            _context = context;
        }
        
        public void CreateInsertTransaction(
            int AccountID,
            string operation,
            int amount,
            string symbol,
            string bank,
            string otheraccount)
        {
            var transaction = new Transactions()
            {
                AccountId = AccountID,
                Amount = amount,
                Operation = operation,
                Symbol = symbol,
                Bank = bank,
                Account = otheraccount,
                Date = DateTime.Now,
                Type = "Credit"                
            };

            var affectedaccount = _context.Accounts.FirstOrDefault(x => x.AccountId == AccountID);

            affectedaccount.Balance += amount;

            transaction.Balance = affectedaccount.Balance;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<Transactions> GetTransactionsByAccountID(int id)
        {
            return _context.Transactions.Where(x => x.TransactionId == id);
        }
    }
}
