using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bank.ValidationAttributes;

namespace Bank.ViewModels
{
    public class InsertViewModel
    {
        public int AccountID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Operation { get; set; }

        //public List<SelectListItem> OperationAlternatives = new List<SelectListItem>();

        [Required]      
        [NotLessThanZero]       //Clientvalidering vid tid
        public int Amount { get; set; }
        [StringLength(50)]
        public string Symbol { get; set; }
        [StringLength(50)]
        public string Bank { get; set; }
        [StringLength(50)]
        public string Account { get; set; }
    }
}
