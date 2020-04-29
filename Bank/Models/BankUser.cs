using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
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
