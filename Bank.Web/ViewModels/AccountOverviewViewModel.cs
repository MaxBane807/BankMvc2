using System.Collections.Generic;

namespace Bank.Web.ViewModels
{
    public class AccountOverviewViewModel
    {
        public int AccountID { get; set; }

        public decimal AccountBalance { get; set; }

        public List<TransactionViewModel> Transactions = new List<TransactionViewModel>();
        
    }
}
