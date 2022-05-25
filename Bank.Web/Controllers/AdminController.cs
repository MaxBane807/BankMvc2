using AutoMapper;
using Bank.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bank.Web.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Identity;
using Bank.Data;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.JSInterop.Infrastructure;

namespace Bank.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IMapper _customerMapper;
        private ICustomerService _customerService;
        private UserManager<BankUser> _userManager;
        public AdminController(IMapper customerMapper, ICustomerService customerService, UserManager<BankUser> userManager)
        {
            _customerMapper = customerMapper;
            _customerService = customerService;
            _userManager = userManager;
        }

        public IActionResult CreateUser()
        {
            var model = new CreateUserViewModel();           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new BankUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Cashier").Wait();
                    return RedirectToAction("ManageUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    
                }
                
            }
            return View(model);
        }
        public async Task<IActionResult> ManageUsers()
        {
            var model = new ManageUsersViewModel();
            
            var users = _userManager.Users.ToList();
            foreach(var user in users)
            {
                var modeluser = new ManageUsersViewModel.UserViewModel();
                var isAdmin = await _userManager.IsInRoleAsync(user,"Admin");
                modeluser.Admin = isAdmin;
                modeluser.ID = user.Id;
                modeluser.Email = user.Email;
                modeluser.FirstName = user.FirstName;
                modeluser.LastName = user.LastName;
                model.Users.Add(modeluser);
            }
            
            return View(model);
        }

        public async Task<IActionResult> ChangeRole(string id, bool admin)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                if (admin)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        var secondresult = await _userManager.AddToRoleAsync(user, "Cashier");
                        if (secondresult.Succeeded)
                        {
                            return RedirectToAction("ManageUsers");
                        }
                        await _userManager.AddToRoleAsync(user, "Admin");
                        return RedirectToAction("ManageUsers");
                    }
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "Cashier");
                    if (result.Succeeded)
                    {
                        var secondresult = await _userManager.AddToRoleAsync(user, "Admin");
                        if (secondresult.Succeeded)
                        {
                            return RedirectToAction("ManageUsers");
                        }
                        await _userManager.AddToRoleAsync(user, "Cashier");
                        return RedirectToAction("ManageUsers");
                    }
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            
        }

        public async Task<IActionResult> ChangeUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = new ChangeUserViewModel()
            {
                Id = id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.Phone;
            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                return RedirectToAction("ManageUsers");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {                
                
                return RedirectToAction("ManageUsers");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}