using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.ViewModels;
using System.Security.Cryptography.Xml;
using Bank.Models;
using Bank.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services.Classes
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
