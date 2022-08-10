using ApiTgBot.Models.EF;
using ApiTgBot.Models.EF.Tables;

namespace ApiTgBot.Controllers
{
    public class ConverterController : IConvert
    {
        IApplicationContext db;

        public ConverterController(IApplicationContext db)
        {
            this.db = db;
        }

        public Currency Convert(CurrencyType inType, decimal money, CurrencyType outType)
        {
            throw new NotImplementedException();
        }
    }
}
