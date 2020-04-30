using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class StartPageViewModel
    {
        public int nrOfCustomers { get; set; }

        public int nrOfAccounts { get; set; }

        public decimal sumOfAllAccounts { get; set; }
       
        public int customerID { get; set; }
    }
}
