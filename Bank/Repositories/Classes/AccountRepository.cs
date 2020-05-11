using Bank.Data;
using Bank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Repositories.Classes
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankAppDataContext _context;
        public AccountRepository(BankAppDataContext context)
        {
            _context = context;
        }
        
        public int getNrOfAccounts()
        {
            int nr = _context.Accounts.Count();
            return nr;
        }

        public decimal getSumOfAccounts()
        {
            decimal sum = _context.Accounts.Sum(x => x.Balance);
            return sum;
        }

        public List<int> getAccountsByCustomerID(int customerid)
        {
            var accountnumbers = _context.Customers
                .Include(x => x.Dispositions)
                .FirstOrDefault(x => x.CustomerId == customerid)
                .Dispositions.Select(y => y.AccountId).ToList();

            return accountnumbers;
        }
        
        public Accounts getAccountByID(int accountid)
        {
            _context.Accounts.FirstOrDefault(x => x.AccountId == accountid);
        }
    }
}
