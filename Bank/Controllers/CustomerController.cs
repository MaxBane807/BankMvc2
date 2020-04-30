using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Bank.ViewModels;
using Bank.Repositories.Interfaces;

namespace Bank.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        
        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepository = customerRepo;
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
            
            
            return View(viewmodel);
        }
    }
}