using System;
using System.Linq;
using Bank.Web.Data;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;

namespace Bank.Web.Services.Classes
{
    public class WithdrawService : IWithdrawService
    {
        private readonly BankAppDataContext _context;
        
        public WithdrawService(BankAppDataContext context)
        {
            _context = context;
        }
        
        public void CreateWithdraw(int AccountID, string operation, decimal amount, string symbol, string bank, string otherAccount)
        {
            var affectedAccount = _context.Accounts.FirstOrDefault(x => x.AccountId == AccountID);
            affectedAccount.Balance -= amount;

            var transaction = new Transactions
            {
                AccountId = AccountID,
                Operation = operation,
                Amount = (amount * -1),
                Symbol = symbol,
                Bank = bank,
                Account = otherAccount,
                Date = DateTime.Now,
                Type = "Debit",
            };

            transaction.Balance = affectedAccount.Balance;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
