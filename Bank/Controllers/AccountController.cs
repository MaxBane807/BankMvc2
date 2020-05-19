using System.Linq;
using Bank.Web.Services.Interfaces;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
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
                .GetandOrderTransactionsByAccountID(id, 20,0)
                .Select(x => new TransactionViewModel
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

        [AcceptVerbs("GET")]
        public IActionResult LoadTwentyMoreTransactions(int accountid, int nrToSkip)
        {
            var viewmodel = new LoadTwentyViewModel();

            var totalcount = _transactionService.CountTransactionsByAccountID(accountid);

            if ((nrToSkip + 1) * 20 >= totalcount)
            {
                viewmodel.AnyMore = false;
            }
            else
            {
                viewmodel.AnyMore = true;
            }

            nrToSkip = nrToSkip * 20;           

            viewmodel.Transactions = _transactionService.GetandOrderTransactionsByAccountID(accountid, 20, nrToSkip)
                .Select(x => new TransactionViewModel
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


            return Json(new {anyMore = viewmodel.AnyMore, transactions = viewmodel.Transactions });
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