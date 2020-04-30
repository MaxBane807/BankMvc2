using Bank.Data;
using Bank.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

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

        public Customers searchCustomerByID(int id)
        {
            var result = _context.Customers.Find(id);
            return result;
        }
        public decimal getTotalAmountByID(int id)
        {
            var result = _context.Customers.FirstOrDefault(x => x.CustomerId == id)
                .Dispositions.Sum(y => y.Account.Balance);

            return result;
        }
    }
}
