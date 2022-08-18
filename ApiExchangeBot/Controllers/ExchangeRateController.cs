using ApiExchangeBot.Models;
using ApiExchangeBot.Models.EF;
using ApiExchangeBot.Models.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiExchangeBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        readonly ApplicationContext db;
        public ExchangeRateController(ApplicationContext db)
        {
            this.db = db;
        }
        // GET: api/<ExchangeRateController>
        [HttpGet]
        public IActionResult Get()
            => Ok((from e in db.ExchangeRates
                   select e).ToList());

        // GET api/<ExchangeRateController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
            => Ok((from e in db.ExchangeRates
                   where e.Id == id
                   select e).FirstOrDefault(new ExchangeRate()));


        // PUT api/<AccountController>/5/Convert
        [HttpPut("{id}/Convert")]
        public IActionResult Convert([FromRoute] int id,
            [FromQuery] CurrencyType inType,
            [FromQuery] decimal amount,
            [FromQuery] CurrencyType outType)
        {
            if (inType == CurrencyType.NotCurrency ||
                amount <= 0 ||
                outType == CurrencyType.NotCurrency)
                return BadRequest();

            Account? account = (from a in db.Accounts
                               .Include(a => a.Wallet)
                               where a.TelegramId == id
                               select a).FirstOrDefault();
            if(account == null)
                return NotFound();

            ExchangeRate? exchangeRate = (from e in db.ExchangeRates
                                          where e.inType == inType || e.inType == outType ||
                                                e.outType == inType || e.outType == outType
                                          select e).FirstOrDefault();
            if (exchangeRate == null)
                return BadRequest();

            AccountFacade accountFacade = new(account);
            Account result = accountFacade.ConvertyMoney(new Currency { Type = inType, Amount = amount }, exchangeRate);
            db.SaveChanges();

            return Ok(result);
        }
    }
}
