using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.ViewModels;

namespace Bank.Services.Interfaces
{
    public interface IInsertService
    {
        void CreateAnInsert(int AccountID,string operation,decimal amount,string symbol,string bank,string otherAccount);
    }
}
