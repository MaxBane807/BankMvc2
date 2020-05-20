using System;
using System.Linq;
using Bank.Web.Data;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Bank.Web.Extensions;

namespace Bank.Web.Services.Classes
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
        public IQueryable<Customers> getListedCustomers(int pagesize, int currentPage, string searchName, string searchCity)
        {
            var query = _context.Customers.AsNoTracking();
            
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.Givenname.Contains(searchName) || x.Surname.Contains(searchName));
            };

            if (!string.IsNullOrEmpty(searchCity))
            {
                query = query.Where(x => x.City.Contains(searchCity));
            };
            
            return query.Skip((currentPage - 1) * pagesize).Take(pagesize);
        }
        public int getNumberOfCustomers()
        {
            return _context.Customers.Count();
        }
        public int getNumberOfCustomersBySearch(string searchName,string searchCity)
        {
            var query = _context.Customers.AsNoTracking();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.Givenname.Contains(searchName) || x.Surname.Contains(searchName));
            };

            if (!string.IsNullOrEmpty(searchCity))
            {
                query = query.Where(x => x.City.Contains(searchCity));
            };

            return query.Count();
        }
    }
}
