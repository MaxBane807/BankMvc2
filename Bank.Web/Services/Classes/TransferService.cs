using System;
using Bank.Web.Services.Interfaces;

namespace Bank.Web.Services.Classes
{
    public class TransferService : ITransferService
    {
        private readonly IInsertService _insertService;
        private readonly IWithdrawService _withdrawService;
        private readonly IAccountService _accountService;
        public TransferService(IInsertService insertService, IWithdrawService withdrawService, IAccountService accountService)
        {
            _insertService = insertService;
            _withdrawService = withdrawService;
            _accountService = accountService;
        }
        
        public bool CreateTransfer(int accountid, bool credit, string operation, decimal amount, string symbol, string otheraccount)
        {
            if (credit)
            {
                if (AmountShouldNotBeGreaterThenExisting(accountid, amount, true, otheraccount))
                {
                    _withdrawService.CreateWithdraw(Int32.Parse(otheraccount), operation, amount, symbol, "This Bank",
                        accountid.ToString());
                    _insertService.CreateAnInsert(accountid, operation, amount, symbol, "This Bank", otheraccount);
                    return true;
                }
            }
            else
            {
                if (AmountShouldNotBeGreaterThenExisting(accountid,amount,false,otheraccount))
                {
                    _withdrawService.CreateWithdraw(accountid, operation, amount, symbol, "This Bank", otheraccount);
                    _insertService.CreateAnInsert(Int32.Parse(otheraccount), operation, amount, symbol, "This Bank", accountid.ToString());
                    return true;
                }
                
            }

            return false;
        }
        private bool AmountShouldNotBeGreaterThenExisting(int accountID, decimal amount, bool credit, string otheraccount)
        {
            if (!credit)
            {
                decimal currentbalance = _accountService.GetAccountBalanceByID(accountID);
                if (amount > currentbalance)
                {
                    return false;
                }
            }
            else
            {
                decimal balanceAtOtherAccount = _accountService.GetAccountBalanceByID(Int32.Parse(otheraccount));
                if (amount > balanceAtOtherAccount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
