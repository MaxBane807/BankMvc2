using Bank.Data;
using Bank.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

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
    }
}
