// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using ApiControllers.Controllers;
using ApiControllers.Models;

Console.WriteLine("Hello, World!");

try
{
    ExchangeRateClient exchangeRateClient = new ExchangeRateClient();

    var res = await exchangeRateClient.Convert(111,CurrencyType.BTC,50,CurrencyType.USD);
    //Console.WriteLine(res);
    Console.WriteLine(JsonSerializer.Serialize(res, new JsonSerializerOptions { WriteIndented = true }));
}
catch (Exception ex)
{
    Console.Write(ex.Message);
}