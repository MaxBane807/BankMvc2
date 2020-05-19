using System.Collections.Generic;
using System.Linq;
using Bank.Web.Data;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Web.Services.Classes
{
    public class AccountService: IAccountService
    {
        private readonly BankAppDataContext _context;
        public AccountService(BankAppDataContext context)
        {
            _context = context;
        }
        
        public Accounts PrepareViewAccount(int id)
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountId == id);
        }
        public List<int> getAccountsByCustomerID(int customerid)
        {
            return _context.Customers
                .Include(x => x.Dispositions)
                .FirstOrDefault(x => x.CustomerId == customerid)
                .Dispositions.Select(y => y.AccountId).ToList();
        }
        public int GetNrOfAccounts()
        {
            return _context.Accounts.Count();           
        }
        public decimal GetSumOfAccounts()
        {
            return _context.Accounts.Sum(x => x.Balance);          
        }
        public decimal GetAccountBalanceByID(int id)
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountId == id).Balance;
        }
        public bool CheckIfAccountExists(int id)
        {
            return _context.Accounts.Any(x => x.AccountId == id);
        }
    }
}
