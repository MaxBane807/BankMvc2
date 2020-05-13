using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Services.Interfaces;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        public AccountController(IAccountService accountService,ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        
        public IActionResult ViewAccount(int id)
        {                   
            var account = _accountService.PrepareViewAccount(id);
            var viewmodel = new AccountOverviewViewModel()
            {
                AccountBalance = account.Balance,
                AccountID = account.AccountId
            };

            viewmodel.Transactions = _transactionService
                .GetandOrderTransactionsByAccountID(id, 20)
                .Select(x => new AccountOverviewViewModel.TransactionViewModel
                {
                    TransactionId = x.TransactionId,
                    Amount = x.Amount,
                    Balance = x.Balance,
                    Bank = x.Bank,
                    Date = x.Date,
                    Operation = x.Operation,
                    OtherAccount = x.Account,
                    Type = x.Type,
                    Symbol = x.Symbol
                }).ToList();

            return View(viewmodel);
        }


        //Fundera på att göra om till custom-attribute
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyNotSameAccountNr(string Account, int AccountID)
        {
            if (Account == AccountID.ToString())
            {
                return Json($"You can't transfer to the same account");
            }

            return Json(true);
        }
    }
}