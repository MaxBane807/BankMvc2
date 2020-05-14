﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class AccountOverviewViewModel
    {
        public int AccountID { get; set; }

        public decimal AccountBalance { get; set; }

        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();
        
    }
}
