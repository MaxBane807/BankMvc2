using Bank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Services.Interfaces
{
    public interface IAccountService
    {
        Accounts PrepareViewAccount(int id);
        public List<int> getAccountsByCustomerID(int customerid);
    }
}
