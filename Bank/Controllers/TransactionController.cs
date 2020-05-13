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
        private readonly ITransferService _TransferService;
        private readonly IAccountService _AccountService;
        
        public TransactionController(
            ITransactionService transactionService,
            IInsertService insertService,
            IWithdrawService withdrawService,
            ITransferService transferService,
            IAccountService accountService)
        {
            _TransactionService = transactionService;
            _InsertService = insertService;
            _WithdrawService = withdrawService;
            _TransferService = transferService;
            _AccountService = accountService;
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
            AmountShouldNotBeGreaterThenExisting(viewmodel.AccountID, viewmodel.Amount, false, false, viewmodel.Account);
            if (ModelState.IsValid)
            {
                _WithdrawService.CreateWithdraw(viewmodel.AccountID, viewmodel.Operation, viewmodel.Amount, viewmodel.Symbol, viewmodel.Bank, viewmodel.Account);
                return RedirectToAction("ViewAccount", "Account", new { id = viewmodel.AccountID });
            }
            return View(viewmodel);
        }
        
        public IActionResult Transfer(int accountID)
        {
            var viewmodel = new TransferViewModel();
            viewmodel.AccountID = accountID;

            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]     
        public IActionResult Transfer(TransferViewModel viewmodel)
        {
            OtherAccountExists(viewmodel.Account);
            AmountShouldNotBeGreaterThenExisting(viewmodel.AccountID, viewmodel.Amount, viewmodel.Credit, true, viewmodel.Account);           
            if (ModelState.IsValid)
            {
                _TransferService.CreateTransfer(
                    viewmodel.AccountID,
                    viewmodel.Credit,
                    viewmodel.Operation,
                    viewmodel.Amount,
                    viewmodel.Symbol,
                    viewmodel.Account);
                return RedirectToAction("ViewAccount", "Account", new { id = viewmodel.AccountID });
            }
            
            return View(viewmodel);
        }

        private void AmountShouldNotBeGreaterThenExisting(int accountID, decimal amount,bool credit, bool transfer, string otheraccount)
        {
            if (!credit)
            {
                decimal currentbalance = _AccountService.GetAccountBalanceByID(accountID);
                if (amount > currentbalance)
                {
                    ModelState.AddModelError(string.Empty, "The account dosn't contain that sum. It contains " + currentbalance);
                }
            }
            else if (transfer)
            {
                decimal balanceAtOtherAccount = _AccountService.GetAccountBalanceByID(Int32.Parse(otheraccount));
                if (amount > balanceAtOtherAccount)
                {
                    ModelState.AddModelError(string.Empty, "The other account dosen't contain that sum. It contains " + balanceAtOtherAccount);
                }
            }
           
        }
        private void OtherAccountExists(string id)
        {            
            int junk;
            bool exists = Int32.TryParse(id, out junk) && _AccountService.CheckIfAccountExists(Int32.Parse(id));
            if (!exists)
            {
                ModelState.AddModelError(string.Empty, "The other account dosen't exist");
            }
        }
    }
}