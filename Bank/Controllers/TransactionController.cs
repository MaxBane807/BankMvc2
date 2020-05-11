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
        
        public TransactionController(ITransactionService transactionService, IInsertService insertService)
        {
            _TransactionService = transactionService;
            _InsertService = insertService;
        }
        
        public IActionResult Insert(int accountid)
        {
            //Kom ihåg: uppdatera balans i account
            //tre typer, credit in cash och collection from an other bank samt interest
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
                return RedirectToAction("ViewAccount", "Account");
            }
            return View(viewmodel);
        }
    }
}