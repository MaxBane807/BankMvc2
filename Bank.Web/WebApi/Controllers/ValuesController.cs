using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Data;
using System.ComponentModel.DataAnnotations;
using Bank.Web.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Bank.Data.Models;
using Bank.Web.WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly BankAppDataContext _context;
        private readonly CustomerService _customerService;
        private readonly AccountService _accountService;

        public ValuesController(BankAppDataContext context, CustomerService customerService, AccountService accountService)
        {
            _context = context;
            _customerService = customerService;
            _accountService = accountService;
        }
        
        // GET: api/accounts/5/
        
        //[HttpGet("accounts/{id}")]
        //public IActionResult Get(int id, [Required,FromQuery] int limit, [Required,FromQuery] int offset)
        //{
            
        //}

        // GET api/me/
        [HttpGet("me")]
        public IActionResult Get()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new MeViewModel()
            {
                Customer = _customerService.getCustomerByUniqueID(id.ToString()),
                AccountNumbers = _accountService.getAccountsByCustomerID(Int32.Parse(id.ToString()))
            };
            
            return Ok(model);
        }

        // POST api/
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
