using Bank.Data;
using Bank.Models;
using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Classes
{
    public class InsertService : IInsertService
    {
        private readonly BankAppDataContext _context;
        public InsertService(BankAppDataContext context)
        {
            _context = context;
        }
        public void CreateAnInsert(int AccountID, string operation, decimal amount, string symbol, string bank, string otherAccount)
        {
            var affectedAccount = _context.Accounts.FirstOrDefault(x => x.AccountId == AccountID);
            affectedAccount.Balance += amount;
            
            var transaction = new Transactions
            {
                AccountId = AccountID,
                Operation = operation,
                Amount = amount,
                Symbol = symbol,
                Bank = bank,
                Account = otherAccount,
                Date = DateTime.Now,
                Type = "Credit",
            };

            transaction.Balance = affectedAccount.Balance;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
