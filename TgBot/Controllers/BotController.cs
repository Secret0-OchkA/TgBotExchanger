using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TgBot.Controllers
{
    public class BotController
    {
        readonly TelegramBotClient client;
        CancellationTokenSource tokenSource;

        public BotController(string key)
        {
            client = new TelegramBotClient(key);
            tokenSource = new CancellationTokenSource();
        }

        public async Task Start()
        {
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: tokenSource.Token);

            var me = await client.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;

            var chatId = message.Chat.Id;

            await PhotoWork(botClient, update, cancellationToken);

            //Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            //// Echo received message text
            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "You said:\n" + messageText,
            //    cancellationToken: cancellationToken);
        }

        async Task PhotoWork(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;

            if (update.Message.Photo != null)
            {
                var fileId = update.Message.Photo.Last().FileId;
                Message messageResponse = await botClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: fileId);
            }
            else if (update.Message.Document != null)
            {
                //string fileId = "BQACAgIAAxkBAAIDv2L1WlCG65zj2EzZzINAa2EMPU8IAAIjHgACl02oS28z4Tk0pGnqKQQ";
                var fileId = update.Message.Document.FileId;
                Message messageResponse = await botClient.SendDocumentAsync(
                    chatId: message.Chat.Id,
                    document: fileId);
            }

            return;
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
