using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.ViewModels.AdminViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [StringLength(256,ErrorMessage ="The login email address is to long")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        [StringLength(40)]
        public string LastName { get; set; }
    }
}
