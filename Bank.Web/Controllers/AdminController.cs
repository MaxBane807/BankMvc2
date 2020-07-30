using AutoMapper;
using Bank.Web.Models;
using Bank.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IMapper _customerMapper;
        private ICustomerService _customerService;
        public AdminController(IMapper customerMapper, ICustomerService customerService)
        {
            _customerMapper = customerMapper;
            _customerService = customerService;
        }

     

     
    }
}