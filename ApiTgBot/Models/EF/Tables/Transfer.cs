using System.ComponentModel.DataAnnotations;

namespace ApiTgBot.Models.EF.Tables
{
    public interface ITransfer
    {
        public void Confirm();
        public void Cancel();
        public void Error(string message);
    }

    public class Transfer //: ITransfer
    {
        #region Database 
        public int Id { get; set; }
        [Required]
        public Account ownerTransfer { get; set; }
        [RegularExpression(@"^\d{6}$")]
        public string Number { get; set; } = "000000";
        [RegularExpression(@"^\d{4}$")]
        public string Code { get; set; } = "0000";
        [Required]
        public Currency Amount { get; set; } = new Currency();

        //photo_Id and files_Id in telegram
        public List<TransferArgument> arguments { get; set; } = new List<TransferArgument> { };
        #endregion

        #region Events
        public TransferEventHandlerConfirm? ConfirmHandle;
        public TransferEventHandlerCancel? CancelHandle;
        public TransferEventHandlerError? ErrorHandle;
        #endregion

        public void Confirm() => ConfirmHandle?.Invoke(Amount);
        public void Cancel() => CancelHandle?.Invoke();
        public void Error(string message) => ErrorHandle?.Invoke(message);
    }

    public delegate void TransferEventHandlerConfirm(Currency money);
    public delegate void TransferEventHandlerCancel();
    public delegate void TransferEventHandlerError(string message);
}
