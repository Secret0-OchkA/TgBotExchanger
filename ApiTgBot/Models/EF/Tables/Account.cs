using ApiTgBot.Models.EF.Tables;

namespace TgBot.Models.EF.Tables
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Currency> Wallet { get; set; }
    }
}
