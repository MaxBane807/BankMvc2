using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class ListCustomersViewModel
    {
        //Lägg till sortering vid tid

        public PagingViewModel PagingViewModel { get; set; } = new PagingViewModel();

        public List<CustomerViewModel> Customers = new List<CustomerViewModel>();

        public class CustomerViewModel
        {
            public int CustomerId { get; set; }

            public string NationalId { get; set; }

            public string Givenname { get; set; }

            public string Surname { get; set; }

            public string Streetaddress { get; set; }
       
            public string City { get; set; }
        }
    }
}
