using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class LoadTwentyViewModel
    {
        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();
        
        public bool AnyMore { get; set; }
        public bool FirstPageLoad { get; set; }
    }
}
