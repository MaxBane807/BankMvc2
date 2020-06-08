using System.ComponentModel.DataAnnotations;
using Bank.Web.ValidationAttributes;

namespace Bank.Web.ViewModels
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
        public decimal Amount { get; set; }
        [StringLength(50)]
        public string Symbol { get; set; }
        [StringLength(50)]
        public string Bank { get; set; }
        [StringLength(50)]
        public string Account { get; set; }
    }
}
