using ApiExchangeBot.Models.EF;
using ApiExchangeBot.Models.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiExchangeBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        readonly ApplicationContext db;
        public ModeratorController(ApplicationContext db)
        {
            this.db = db;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok((from t in db.Transfers
                        .Include(t => t.ownerTransfer)
                           .ThenInclude(a => a.Wallet)
                        .Include(t => t.Amount)
                        .Include(t => t.arguments)
                       select t).ToList());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(GetTransfer(id));

        // PUT api/<ValuesController>/5/Confirm
        [HttpPut("{id}/Confirm")]
        public IActionResult ConfirmTransfer(int id)
        {
            Transfer transfer = GetTransfer(id);

            if (transfer.ownerTransfer.TelegramId == 0)
                return BadRequest();
            if (transfer.Confirmed)
                return BadRequest("this operation confirm");

            transfer.Confirm();

            db.SaveChanges();

            return Ok(transfer.ownerTransfer);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Transfer transfer = GetTransfer(id);
            if (transfer.ownerTransfer.TelegramId == 0)
                return BadRequest();

            db.Transfers.Remove(transfer);
            db.SaveChanges();

            return Ok(transfer);
        }

        Transfer GetTransfer(int Id)
        {
            List<Transfer> transfers = (from t in db.Transfers
                                    .Include(t => t.ownerTransfer)
                                       .ThenInclude(a => a.Wallet)
                                    .Include(t => t.Amount)
                                    .Include(t => t.arguments)
                                  where t.Id == Id
                                  select t).ToList();
            if (transfers.Count == 0)
                return new Transfer();
            return transfers[0];
        }
    }
}
