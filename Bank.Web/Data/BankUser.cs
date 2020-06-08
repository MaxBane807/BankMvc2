using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bank.Web.Data
{
    public partial class BankUser : IdentityUser
    {      
        //Overrida tidigare constraints?
        
        [StringLength(40)]
        public string FirstName { get; set; }
        [StringLength(40)]
        public string LastName { get; set; }
    }
}
