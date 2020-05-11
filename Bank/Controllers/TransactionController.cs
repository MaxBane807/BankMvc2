using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly _
        
        public TransactionController()
        {

        }
        
        public IActionResult Insert(int accountid)
        {
            //Kom ihåg: uppdatera balans i account
            //tre typer, credit in cash och collection from an other bank samt interest
            var viewmodel = new InsertViewModel();
            viewmodel.AccountID = accountid;           

            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(InsertViewModel viewmodel)
        {
            return View();
        }
    }
}