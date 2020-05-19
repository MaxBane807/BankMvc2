using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<BankUser> _signInManager;
        public UserController(SignInManager<BankUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public IActionResult Login()
        {
            var viewmodel = new LoginViewModel();
            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return View("LockedOut");
                }
            }
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}