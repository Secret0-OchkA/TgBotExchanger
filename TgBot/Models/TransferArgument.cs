namespace TgBot.Models
{
    public class TransferArgument
    {
        public int Id { get; set; }
        public TransferArgumentType Type { get; set; } = TransferArgumentType.NullArgument;
        public string FileId { get; set; } = "NullArgument";
    }

    public enum TransferArgumentType
    {
        NullArgument,
        Document,
        Photo
    }
}
