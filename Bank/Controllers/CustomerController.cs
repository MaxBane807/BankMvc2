using System;
using System.Linq;
using Bank.Web.Services.Interfaces;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        
        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }            

        public IActionResult viewCustomer(string customerID)
        {
            var customer = _customerService.getCustomerByID(customerID);

            if (customer != null)
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

                viewmodel.TotalAmount = _customerService.getTotalAmountByID(customer.CustomerId);
                viewmodel.Accounts = _accountService.getAccountsByCustomerID(customer.CustomerId);

                return View(viewmodel);
            }
            else
            {
                return View("CustomerNotFound");
            }
        }

        public IActionResult ListCustomers(string page,string searchName, string searchCity)
        {
                      
            var listCustomersViewModel = new ListCustomersViewModel();

            listCustomersViewModel.PagingViewModel.CurrentPage = string.IsNullOrEmpty(page) ? 1 : Convert.ToInt32(page);

            listCustomersViewModel.PagingViewModel.PageSize = 50;

            listCustomersViewModel.Customers = _customerService.getListedCustomers(
                listCustomersViewModel.PagingViewModel.PageSize,
                listCustomersViewModel.PagingViewModel.CurrentPage,
                searchName,
                searchCity
                ).Select(x => new ListCustomersViewModel.CustomerViewModel
                {
                    CustomerId = x.CustomerId,
                    City = x.City,
                    Givenname = x.Givenname,
                    NationalId = x.NationalId,
                    Streetaddress = x.Streetaddress,
                    Surname = x.Surname
                }).ToList();

            var pageCount = (double)_customerService.getNumberOfCustomersBySearch(searchName,searchCity) / listCustomersViewModel.PagingViewModel.PageSize;
            listCustomersViewModel.PagingViewModel.MaxPages = (int)Math.Ceiling(pageCount);

            return View(listCustomersViewModel);
        }
    }
}