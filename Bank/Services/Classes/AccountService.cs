﻿using Bank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.ViewModels;
using Bank.Repositories.Classes;
using Bank.Repositories.Interfaces;
using System.Security.Cryptography.Xml;
using Bank.Models;
using Bank.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services.Classes
{
    public class AccountService: IAccountService
    {
        private readonly BankAppDataContext _context;
        public AccountService(BankAppDataContext context)
        {
            _context = context;
        }
        
        public Accounts PrepareViewAccount(int id)
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountId == id);
        }
        public List<int> getAccountsByCustomerID(int customerid)
        {
            return _context.Customers
                .Include(x => x.Dispositions)
                .FirstOrDefault(x => x.CustomerId == customerid)
                .Dispositions.Select(y => y.AccountId).ToList();
        }
    }
}