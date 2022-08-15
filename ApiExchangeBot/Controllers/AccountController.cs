using ApiExchangeBot.Models;
using ApiExchangeBot.Models.EF;
using ApiExchangeBot.Models.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiExchangeBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly ApplicationContext db;
        public AccountController(ApplicationContext db)
        {
            this.db = db;
        }

        // GET api/<AccountController>/Ok
        [HttpGet("Ok")]
        public IActionResult OK() => Content("Ok");

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(GetAccount(id));

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Post(int id, string name)
        {
            if (id == null || name == null)
                return BadRequest();

            Account account = new Account()
            {
                TelegramId = id,
                Name = name,
            };
            db.Accounts.Add(account);
            db.SaveChanges();

            return Ok(account);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Account account = GetAccount(id);

            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();

                return Ok(account);
            }
            return BadRequest();
        }

        // POST api/<AccountController>/5/Deposit
        [HttpPost("{id}/Deposit")]
        public IActionResult Deposit([FromRoute] int id,
            [FromBody] Currency currency,
            string number,
            string code)
        {
            AccountFacade account = new(GetAccount(id));

            Transfer transfer = account.Deposit(currency, number, code);

            db.Transfers.Add(transfer);
            db.SaveChanges();

            return Ok(transfer);
        }

        // POST api/<AccountController>/5/Withdraw
        [HttpPost("{id}/Withdraw")]
        public IActionResult Withdraw([FromRoute] int id, [FromBody] Currency currency)
        {
            Account account = GetAccount(id);
            AccountFacade facade = new(account);

            if(account.TelegramId == 0)
                return BadRequest(account);

            try
            {
                facade.Withdraw(currency);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            db.SaveChanges();
            return Ok(account);
        }

        Account GetAccount(int id)
        {
            List<Account> accounts = (from a in db.Accounts.Include(a => a.Wallet)
                                     where a.TelegramId == id
                                     select a).ToList();

            if (accounts.Count == 0)
                return new Account();
            
            return accounts[0];
        }
    }
}
