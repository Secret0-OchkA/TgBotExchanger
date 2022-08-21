using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TgBot.Models;

namespace TgBot.Clients
{
    public class ModeratorClient
    {
        static readonly HttpClient _httpClient;
        static ModeratorClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new($"https://Localhost:7180/api/Moderator/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async ValueTask<IEnumerable<Transfer>> Get()
        {
            List<Transfer>? transfers = await _httpClient.GetFromJsonAsync<List<Transfer>>("");
            return transfers != null ? transfers : new List<Transfer>();
        }
        public async ValueTask<Transfer> Get(int id)
        {
            Transfer? transfer = await _httpClient.GetFromJsonAsync<Transfer>($"{id}");
            return transfer != null ? transfer : new Transfer();
        }
        public async ValueTask<Account> ConfirmTransfer(int id)
        {
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{id}/Confirm", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Account? account = JsonConvert.DeserializeObject<Account>(responseBody);
            return account != null ? account : new Account();
        }
        public async Task Delete(int id)
            => await _httpClient.DeleteAsync($"{id}");
    }
}
