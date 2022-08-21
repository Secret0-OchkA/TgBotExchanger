namespace TgBot.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int TelegramId { get; set; } = 0;
        public string Name { get; set; } = "Not Account";
        public List<Currency> Wallet { get; set; } = new List<Currency>();
    }
}
