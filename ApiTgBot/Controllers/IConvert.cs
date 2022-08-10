using ApiTgBot.Models.EF.Tables;

namespace ApiTgBot.Controllers
{
    public interface IConvert
    {
        public Currency Convert(CurrencyType inType, decimal money, CurrencyType outType);
    }
}
