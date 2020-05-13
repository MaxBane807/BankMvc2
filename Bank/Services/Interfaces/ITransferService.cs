using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface ITransferService
    {
        void CreateTransfer(int accountid,bool credit,string operation,decimal amount,string symbol,string otheraccount);
    }
}
