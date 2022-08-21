using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ApiControllers.Models;

namespace ApiControllers.Controllers
{
    public class AccountClient
    {
        static readonly HttpClient _httpClient;
        static AccountClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new ($"https://Localhost:7180/api/Account/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async ValueTask<Account> Get(int id)
        {
            Account? res = await _httpClient.GetFromJsonAsync<Account>($"{id}");
            return res != null ? res : new Account();
        }
        public async ValueTask<Account> Post(int id, string name)
        { 
            Account? res = await _httpClient.GetFromJsonAsync<Account>($"?id={id}&Name={name}");
            return res != null ? res : new Account();
        }

        public async Task Delete(int id)
            => await _httpClient.DeleteAsync($"{id}");
        
        public async ValueTask<Transfer> Deposit(int id, Currency money, string number, string code)
        {
            string moneyJson = JsonSerializer.Serialize(money);

            HttpContent content = new StringContent(moneyJson,Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _httpClient.PostAsync($"{id}/Deposit?number={number}&code={code}", content);
            Transfer? transfer = await res.Content.ReadFromJsonAsync<Transfer>();
            return transfer != null ? transfer : new Transfer();
        }
        public async ValueTask<Account> Withdraw(int id, Currency money)
        {
            string moneyJson = JsonSerializer.Serialize(money);

            HttpContent content = new StringContent(moneyJson, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _httpClient.PostAsync($"{id}/Withdraw", content);
            Account? account = await res.Content.ReadFromJsonAsync<Account>();
            return account != null ? account : new Account();
        }
    }
}
