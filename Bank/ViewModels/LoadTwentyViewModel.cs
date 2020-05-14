using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class LoadTwentyViewModel
    {
        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();
        
        public bool AnyMore { get; set; }
    }
}
