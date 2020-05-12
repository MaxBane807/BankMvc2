using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Bank.Services.Interfaces;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _TransactionService;
        private readonly IInsertService _InsertService;
        private readonly IWithdrawService _WithdrawService;
        
        public TransactionController(ITransactionService transactionService, IInsertService insertService, IWithdrawService withdrawService)
        {
            _TransactionService = transactionService;
            _InsertService = insertService;
            _WithdrawService = withdrawService;
        }
        
        public IActionResult Insert(int accountid)
        {          
            var viewmodel = new InsertViewModel();
            viewmodel.AccountID = accountid;           

            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(InsertViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                _InsertService.CreateAnInsert(viewmodel.AccountID, viewmodel.Operation, viewmodel.Amount, viewmodel.Symbol, viewmodel.Bank, viewmodel.Account);
                return RedirectToAction("ViewAccount", "Account", new { id = viewmodel.AccountID });
            }
            return View(viewmodel);
        }
        public IActionResult Withdraw(int accountID)
        {
            var viewmodel = new InsertViewModel();
            viewmodel.AccountID = accountID;
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(InsertViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                _WithdrawService.CreateWithdraw(viewmodel.AccountID, viewmodel.Operation, viewmodel.Amount, viewmodel.Symbol, viewmodel.Bank, viewmodel.Account);
                return RedirectToAction("ViewAccount", "Account", new { id = viewmodel.AccountID });
            }
            return View(viewmodel);
        }
    }
}