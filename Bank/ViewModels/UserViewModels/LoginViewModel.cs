using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
