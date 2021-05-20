using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Bank.Data;
using Bank.Web.Extensions;
using Bank.Data.Models;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Bank.Web.ServiceModels.CustomerServiceModels;
using System.Collections.Generic;
using Bank.Search;
using System.Threading.Tasks;
using Bank.Web.WebApi.Helpers;

namespace Bank.Web.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;
        public CustomerService(BankAppDataContext context, IMapper mapper, ISearchService manageSearch)
        {
            _context = context;
            _mapper = mapper;
            _searchService = manageSearch;
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
        public List<Customers> getListedCustomers(List<int> customerIds)
        {
            var customers = new List<Customers>();
            
            foreach (var Id in customerIds)
            {
                var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == Id);                
                customers.Add(customer);
            }
            return customers;
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

            _searchService.CreateCustomerData(_mapper.Map<Customers, CustomerIndex>(newCustomer));

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

                _searchService.UpdateCustomerData(_mapper.Map<Customers, CustomerIndex>(customer));
            }
            catch (InvalidOperationException e)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                _searchService.CreateCustomerData(_mapper.Map<Customers, CustomerIndex>(customer));
            }
        }

        public Customers GetCustomerByNationalId(string nationalId)
        {
            return _context.Customers.FirstOrDefault(x => x.NationalId == nationalId);
        }

        public async Task<Customers> Authenticate(string username, string password)
        {
            var customer = await Task.Run(() => _context.Customers.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (customer == null)
                return null;

            return customer.WithoutPassword();
        }
    }
}
