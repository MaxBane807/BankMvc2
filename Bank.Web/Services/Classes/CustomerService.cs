using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Bank.Web.Data;
using Bank.Web.Extensions;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Bank.Web.ServiceModels.CustomerServiceModels;

namespace Bank.Web.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;
        public CustomerService(BankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Customers getCustomerByUniqueID(string uniqueId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.UniqueId == uniqueId);
            if (customer == null)
            {
                customer = _context.Customers.FirstOrDefault(x => x.CustomerId == Int32.Parse(uniqueId));
            }
            return customer;
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

        public string CreateCustomer(CreateCustomerServiceModel model)
        {
            //Den nya kunden ska också få ett "startkonto"
            
            Customers newCustomer = _mapper.Map<CreateCustomerServiceModel, Customers>(model);
            newCustomer.UniqueId = Guid.NewGuid().ToString();
            _context.Customers.Add(newCustomer);

            Accounts newAccount = new Accounts() {Balance = 0, Created = DateTime.Today, Frequency = "Monthly"};
            _context.Accounts.Add(newAccount);

            _context.SaveChanges();

            Dispositions newDisposition = new Dispositions()
                {AccountId = newAccount.AccountId, CustomerId = newCustomer.CustomerId, Type = "OWNER"};

            _context.Dispositions.Add(newDisposition);
            _context.SaveChanges();

            return newCustomer.UniqueId;
        }

        public void ChangeCustomer(ChangeCustomerServiceModel model)
        {
            var customer = _mapper.Map<ChangeCustomerServiceModel, Customers>(model);         
            try
            {                
                if (_context.Customers.Any(x => x.CustomerId == customer.CustomerId) == false)
                {
                    throw new InvalidOperationException("The customer that the program tried to change diden't exist. A new customer was added");
                }
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();                
            }
            catch (InvalidOperationException e)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
        }

        public Customers GetCustomerByNationalId(string nationalId)
        {
            return _context.Customers.FirstOrDefault(x => x.NationalId == nationalId);
        }
    }
}
