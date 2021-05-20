using Bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.WebApi.ViewModels
{
    public class MeViewModel
    {
        public Customers Customer { get; set; }
        public List<int> AccountNumbers { get; set; }
    }
}
