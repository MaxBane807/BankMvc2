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
        private readonly IAccountService _accountService;
        
        public WithdrawService(BankAppDataContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        
        public void CreateWithdraw(int AccountID, string operation, decimal amount, string symbol, string bank, string otherAccount)
        {
            if (CheckAmountIsPositive(amount))
            {
                if (AmountShouldNotBeGreaterThenExisting(AccountID, amount))
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

                throw new ArgumentException("Withdrawal exceeds balance", nameof(amount));
            }
            
            throw new ArgumentException("Value can't be negative",nameof(amount));
            
        }
        private bool AmountShouldNotBeGreaterThenExisting(int accountID, decimal amount)
        {
            
            decimal currentBalance = _accountService.GetAccountBalanceByID(accountID);
            if (amount > currentBalance)
            {
                return false;
            }
            
            return true;
        }

        private bool CheckAmountIsPositive(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
