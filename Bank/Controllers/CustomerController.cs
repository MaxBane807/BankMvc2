using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Bank.ViewModels;
using Bank.Repositories.Interfaces;
using Bank.Extensions;

namespace Bank.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        public CustomerController(ICustomerRepository customerRepo, IAccountRepository accountRepo)
        {
            _customerRepository = customerRepo;
            _accountRepository = accountRepo;
        }
        
        
        public IActionResult searchCustomer(string id)
        {
            if (!id.IsInteger())
            {
                return View("CustomerNotFound");
            }
            
            var result = _customerRepository.searchCustomerByID(Int32.Parse(id));

            if (result != null)
            {
                return RedirectToAction("viewCustomer", "Customer", result);
            }
            else
            {
                return View("CustomerNotFound");
            }
        }

        public IActionResult viewCustomer(Customers customer)
        {
            var viewmodel = new CustomerOverviewViewModel()
            {
                Birthday = customer.Birthday,
                City = customer.City,
                Country = customer.Country,
                CountryCode = customer.CountryCode,
                Emailaddress = customer.Emailaddress,
                Gender = customer.Gender,
                Givenname = customer.Givenname,
                NationalId = customer.NationalId,
                Streetaddress = customer.Streetaddress,
                Surname = customer.Surname,
                Telephonecountrycode = customer.Telephonecountrycode,
                Telephonenumber = customer.Telephonenumber,
                Zipcode = customer.Zipcode
            };

            viewmodel.TotalAmount = _customerRepository.getTotalAmountByID(customer.CustomerId);
            viewmodel.Accounts = _accountRepository.getAccountsByCustomerID(customer.CustomerId);
            
            return View(viewmodel);
        }

        public IActionResult ListCustomers(string page)
        {
                      
            var listCustomersViewModel = new ListCustomersViewModel();

            listCustomersViewModel.PagingViewModel.CurrentPage = string.IsNullOrEmpty(page) ? 1 : Convert.ToInt32(page);

            listCustomersViewModel.PagingViewModel.PageSize = 50;

            listCustomersViewModel.Customers = _customerRepository.getListedCustomers(
                listCustomersViewModel.PagingViewModel.PageSize,
                listCustomersViewModel.PagingViewModel.CurrentPage
                ).ToList();    

            var pageCount = (double)_customerRepository.getNumberOfCustomers() / listCustomersViewModel.PagingViewModel.PageSize;
            listCustomersViewModel.PagingViewModel.MaxPages = (int)Math.Ceiling(pageCount);

            return View(listCustomersViewModel);
        }
    }
}