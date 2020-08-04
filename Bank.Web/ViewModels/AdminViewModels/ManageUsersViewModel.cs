using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.ViewModels.AdminViewModels
{
    public class ManageUsersViewModel
    {
        public List<UserViewModel> Users = new List<UserViewModel>();
        
        public class UserViewModel
        {
            public bool Admin { get; set; }
            public string ID { get; set; }           
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
