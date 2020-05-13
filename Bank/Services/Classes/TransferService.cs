using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Classes
{
    public class TransferService : ITransferService
    {
        private readonly IInsertService _insertService;
        private readonly IWithdrawService _withdrawService;
        public TransferService(IInsertService insertService, IWithdrawService withdrawService)
        {
            _insertService = insertService;
            _withdrawService = withdrawService;
        }
        
        public void CreateTransfer(int accountid, bool credit, string operation, decimal amount, string symbol, string otheraccount)
        {
            if (credit == true)
            {
                _withdrawService.CreateWithdraw(Int32.Parse(otheraccount), operation, amount, symbol, "This Bank", accountid.ToString());
                _insertService.CreateAnInsert(accountid, operation, amount, symbol, "This Bank", otheraccount);
            }
            else
            {
                _withdrawService.CreateWithdraw(accountid, operation, amount, symbol, "This Bank", otheraccount);
                _insertService.CreateAnInsert(Int32.Parse(otheraccount), operation, amount, symbol, "This Bank", accountid.ToString());
            }
        }
    }
}
