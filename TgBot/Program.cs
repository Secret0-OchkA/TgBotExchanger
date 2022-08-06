using TgBot.Controllers;

var builder = WebApplication.CreateBuilder(args);

string key = builder.Configuration.GetSection("BotKeys")["test"];


BotController botController = new BotController(key);
await botController.Start();
botController.Stop();