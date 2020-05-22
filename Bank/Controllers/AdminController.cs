using Microsoft.AspNetCore.Mvc;

namespace Bank.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Administer()
        {
            return View();
        }
    }
}