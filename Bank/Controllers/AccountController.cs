using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Repositories.Interfaces;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountrepo)
        {
            _accountRepository = accountrepo;
        }
        
        public IActionResult ViewAccount(int id)
        {
            var viewmodel = new AccountOverviewViewModel();
            var account = _accountRepository.getAccountByID(id);
            viewmodel.AccountID = account.AccountId;
            viewmodel.AccountBalance = account.Balance;
            viewmodel.Transactions = account.Transactions.Select(x => new AccountOverviewViewModel.TransactionViewModel
            {
                Amount = x.Amount,
                Balance = x.Balance,
                Symbol = x.Symbol,
                Bank = x.Bank,
                Date = x.Date,
                Operation = x.Operation,
                TransactionId = x.TransactionId,
                Type = x.Type,
                OtherAccount = x.Account
            }).OrderByDescending(x => x.Date).Take(20).
            ToList();

            return View(viewmodel);
        }
    }
}