using System;
using System.Linq;
using AutoMapper;
using Bank.Web.Data;
using Bank.Web.Extensions;
using Bank.Web.Models;
using Bank.Web.ServiceModels.AdminServiceModels;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Web.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _customerMapper;
        public CustomerService(BankAppDataContext context, IMapper customerMapper)
        {
            _context = context;
            _customerMapper = customerMapper;
        }
        
        public Customers getCustomerByID(string id)
        {
            if (!id.IsInteger())
            {
                return null;
            }
            return _context.Customers.Find(Int32.Parse(id));
        }

        public decimal getTotalAmountByID(string id)
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

        public void CreateCustomer(CreateCustomerServiceModel model)
        {
            //Den nya kunden ska också få ett "startkonto"
            model.CustomerId = Guid.NewGuid().ToString();
            Customers newCustomer = _customerMapper.Map<CreateCustomerServiceModel, Customers>(model);
            _context.Customers.Add(newCustomer);

            Accounts newAccount = new Accounts() {Balance = 0, Created = DateTime.Today, Frequency = "Monthly"};
            _context.Accounts.Add(newAccount);

            _context.SaveChanges();

            Dispositions newDisposition = new Dispositions()
                {AccountId = newAccount.AccountId, CustomerId = model.CustomerId, Type = "OWNER"};

            _context.Dispositions.Add(newDisposition);
            _context.SaveChanges();
        }

        public Customers GetCustomerByNationalId(string nationalId)
        {
            return _context.Customers.FirstOrDefault(x => x.NationalId == nationalId);
        }
    }
}
