using System.ComponentModel.DataAnnotations;
using Bank.Web.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.ViewModels
{
    public class TransferViewModel
    {
        public int AccountID { get; set; }

        [Required]
        public bool Credit { get; set; }

        [Required]
        [StringLength(50)]
        public string Operation { get; set; }

        //public List<SelectListItem> OperationAlternatives = new List<SelectListItem>();

        [Required]
        [NotLessThanZero]       //Clientvalidering vid tid
        public decimal Amount { get; set; }
        [StringLength(50)]
        public string Symbol { get; set; }
        
        [Required]
        [StringLength(50)]
        [Remote(action: "VerifyNotSameAccountNr", controller: "Account", AdditionalFields = nameof(AccountID))]
        public string Account { get; set; }
    }
}
