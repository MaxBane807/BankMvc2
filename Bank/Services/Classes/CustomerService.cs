using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Bank.Extensions;
using Bank.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _context;
        public CustomerService(BankAppDataContext context)
        {
            _context = context;
        }
        
        public Customers getCustomerByID(string id)
        {
            if (!id.IsInteger())
            {
                return null;
            }
            return _context.Customers.Find(Int32.Parse(id));
        }

        public decimal getTotalAmountByID(int id)
        {
            return _context.Customers.Include(z => z.Dispositions)
                .ThenInclude(l => l.Account)
                .FirstOrDefault(x => x.CustomerId == id)
                .Dispositions.Sum(y => y.Account.Balance);
        }
        public IQueryable<Customers> getListedCustomers(int pagesize, int currentPage)
        {
            return _context.Customers.Skip((currentPage - 1) * pagesize).Take(pagesize);
        }
        public int getNumberOfCustomers()
        {
            return _context.Customers.Count();
        }
    }
}
