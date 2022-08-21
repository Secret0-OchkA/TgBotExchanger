namespace TgBot.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public Account ownerTransfer { get; set; } = new Account();
        public string Number { get; set; } = "000000";
        public string Code { get; set; } = "0000";
        public Currency Amount { get; set; } = new Currency();
        public bool Confirmed { get; set; } = false;
        public List<TransferArgument> arguments { get; set; } = new List<TransferArgument> { };
    }
}
