using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bank.Models;
using Bank.Repositories.Interfaces;
using Bank.ViewModels;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public HomeController(
            ILogger<HomeController> logger,
            ICustomerRepository customerRepo,
            IAccountRepository accountRepo)
        {
            _logger = logger;
            _customerRepository = customerRepo;
            _accountRepository = accountRepo;
        }

        public IActionResult Index()
        {
            var viewModel = new StartPageViewModel();
            viewModel.nrOfCustomers = _customerRepository.getNumberOfCustomers();
            viewModel.nrOfAccounts = _accountRepository.getNrOfAccounts();
            viewModel.sumOfAllAccounts = _accountRepository.getSumOfAccounts();

            return View(viewModel);
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
