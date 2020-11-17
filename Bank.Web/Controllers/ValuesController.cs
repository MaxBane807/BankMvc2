using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Data;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly BankAppDataContext _context;

        public ValuesController(BankAppDataContext context)
        {
            _context = context;
        }
        
        // GET: api/accounts/5/
        
        [HttpGet("accounts/{id}")]
        public IActionResult Get(int id, [Required,FromQuery] int limit, [Required,FromQuery] int offset)
        {
            
        }

        // GET api/5/
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
