using System;
using System.Linq;
using AutoMapper;
using Bank.Web.ServiceModels.CustomerServiceModels;
using Bank.Web.Services.Interfaces;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bank.Data.Models;
using Bank.Search;
using System.Collections.Generic;

namespace Bank.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;
        
        public CustomerController(ICustomerService customerService, IAccountService accountService, IMapper mapper, ISearchService search)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
            _searchService = search;
        }            

        public IActionResult viewCustomer(string searchID)
        {
            var customer = _customerService.getCustomerByUniqueID(searchID);

            if (customer != null)
            {
                var viewmodel = new CustomerOverviewViewModel()
                {                    
                    CustomerId = customer.CustomerId,
                    UniqueId = customer.UniqueId,
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

        public IActionResult ListCustomers(string page,string searchName, string searchCity, string sortColumn, bool asc)
        {
                      
            var listCustomersViewModel = new ListCustomersViewModel();

            int currentPage = string.IsNullOrEmpty(page) ? 1 : Convert.ToInt32(page);
            int pageSize = 50;

            listCustomersViewModel.PagingViewModel.CurrentPage = currentPage;
            listCustomersViewModel.PagingViewModel.PageSize = pageSize;

            var search = searchName + " " + searchCity;
            var searchresult = _searchService.GetPagedCustomerIds(search, sortColumn, asc, pageSize, currentPage);

            listCustomersViewModel.Customers = _mapper.Map<List<Customers>,List<ListCustomersViewModel.CustomerViewModel>>(_customerService.getListedCustomers(searchresult.PagedResultIds));
            
            var pageCount = (double)searchresult.ResultCount / listCustomersViewModel.PagingViewModel.PageSize;
            listCustomersViewModel.PagingViewModel.MaxPages = (int)Math.Ceiling(pageCount);

            return View(listCustomersViewModel);
        }

        public IActionResult CreateCustomer()
        {
            var model = new CreateCustomerViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(CreateCustomerViewModel model)
        {
            CustomerAlreadyRegistered(model.NationalId);
            if (ModelState.IsValid)
            {
                string uniqueId = _customerService.CreateCustomer(_mapper.Map<CreateCustomerViewModel, CreateCustomerServiceModel>(model));
                return RedirectToAction("viewCustomer", new { searchID = uniqueId });
            }

            return View(model);
        }


        public IActionResult ChangeCustomer(CustomerOverviewViewModel model)
        {           
            if (ModelState.IsValid)
            {
                var servicemodel = _mapper.Map<CustomerOverviewViewModel, ChangeCustomerServiceModel>(model);
                _customerService.ChangeCustomer(servicemodel);            
                model.CustomerChanged = true;                         
            }
            else
            {
                model.CustomerChanged = false;               
            }

            model.Accounts = _accountService.getAccountsByCustomerID(model.CustomerId);
            return View("viewCustomer", model);
        }

        private void CustomerAlreadyRegistered(string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId) == false)
            {
                Customers customer = _customerService.GetCustomerByNationalId(nationalId);
                if (customer != null)
                {
                    ModelState.AddModelError(string.Empty, "Customer already registered");
                }
            }
        }
    }
}