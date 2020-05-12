using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bank.Models;
using Bank.ViewModels;
using Bank.Services.Interfaces;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public HomeController(
            ILogger<HomeController> logger,
            ICustomerService customerService,
            IAccountService accountService)
        {
            _logger = logger;
            _customerService = customerService;
            _accountService = accountService;
        }

        [ResponseCache(Duration = 30,Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var viewModel = new StartPageViewModel();
            viewModel.nrOfCustomers = _customerService.getNumberOfCustomers();
            viewModel.nrOfAccounts = _accountService.GetNrOfAccounts();
            viewModel.sumOfAllAccounts = _accountService.GetSumOfAccounts();

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
