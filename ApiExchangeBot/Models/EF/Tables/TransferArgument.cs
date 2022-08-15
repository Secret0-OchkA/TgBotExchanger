using System.ComponentModel.DataAnnotations;

namespace ApiExchangeBot.Models.EF.Tables
{
    public class TransferArgument
    {
        public int Id { get; set; }
        public TransferArgumentType Type { get; set; } = TransferArgumentType.NullArgument;
        [Required]
        public string FileId { get; set; } = "NullArgument";
    }

    public enum TransferArgumentType
    {
        NullArgument,
        Document,
        Photo
    }
}
