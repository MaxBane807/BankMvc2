using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    public interface IWithdrawService
    {
        void CreateWithdraw(int AccountID, string operation, decimal amount, string symbol, string bank, string otherAccount);
    }
}
