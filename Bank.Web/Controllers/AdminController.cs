using AutoMapper;
using Bank.Web.Models;
using Bank.Web.ServiceModels.AdminServiceModels;
using Bank.Web.Services.Interfaces;
using Bank.Web.ViewModels.AdminViewModels;
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
                _customerService.CreateCustomer(_customerMapper.Map<CreateCustomerViewModel,CreateCustomerServiceModel>(model));
            }
            
            return View(model);
        }

        private void CustomerAlreadyRegistered(string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId) == false)
            {
               Customers customer = _customerService.GetCustomerByNationalId(nationalId);
               if (customer != null)
               {
                   ModelState.AddModelError(string.Empty,"Customer already registered");
               } 
            }
        }
    }
}