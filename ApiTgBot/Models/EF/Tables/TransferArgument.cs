using System.ComponentModel.DataAnnotations;

namespace ApiTgBot.Models.EF.Tables
{
    public class TransferArgument
    {
        public int Id { get; set; }
        public TransferArgumentType Type { get; set; }
        [Required]
        public string FileId { get; set; } = "Error";
    }

    public enum TransferArgumentType
    {
        Document,
        Photo
    }
}
