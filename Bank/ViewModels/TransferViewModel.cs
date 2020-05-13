using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bank.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Bank.ViewModels
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
