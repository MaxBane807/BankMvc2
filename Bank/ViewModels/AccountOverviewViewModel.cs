using System;
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

        public class TransactionViewModel
        {
            public int TransactionId { get; set; }

            public DateTime Date { get; set; }
            
            public string Type { get; set; }
            
            public string Operation { get; set; }
            
            public decimal Amount { get; set; }
            
            public decimal Balance { get; set; }
            
            public string Symbol { get; set; }
           
            public string Bank { get; set; }

            public string OtherAccount { get; set; }
        }
    }
}
