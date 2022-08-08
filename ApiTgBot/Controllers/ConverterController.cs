using ApiTgBot.Models.EF.Tables;

namespace ApiTgBot.Controllers
{
    public class ConverterController
    {
        IApplcationContext db;

        public ConverterController(IApplcationContext db)
        {
            this.db = db;
        }
    }
}
