using Bank.Data;
using Bank.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories.Classes
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankAppDataContext _context;
        
        public CustomerRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public int getNumberOfCustomers()
        {
            int nr = _context.Customers.Count();
            return nr;
        }
    }
}
