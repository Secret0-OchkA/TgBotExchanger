using System.ComponentModel.DataAnnotations;

namespace ApiExchangeBot.Models.EF.Tables
{
    public interface ITransfer
    {
        public void Confirm();
        public bool IsConfirmed();
    }

    public class Transfer : ITransfer
    {
        #region Database 
        public int Id { get; set; }
        [Required]
        public Account ownerTransfer { get; set; } = new Account();
        [RegularExpression(@"^\d{6}$")]
        public string Number { get; set; } = "000000";
        [RegularExpression(@"^\d{4}$")]
        public string Code { get; set; } = "0000";
        [Required]
        public Currency Amount { get; set; } = new Currency();

        public bool Confirmed { get; set; } = false;
        //photo_Id and files_Id in telegram
        public List<TransferArgument> arguments { get; set; } = new List<TransferArgument> { };
        #endregion

        public void Confirm()
        {
            Confirmed = true;
            ownerTransfer.Deposit(Amount);
        }

        public bool IsConfirmed() => Confirmed;
    }
}
