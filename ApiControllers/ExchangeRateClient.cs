using ApiControllers.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiControllers.Controllers
{
    public class ExchangeRateClient
    {
        static readonly HttpClient _httpClient;
        static ExchangeRateClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new($"https://Localhost:7180/api/ExchangeRate/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async ValueTask<IEnumerable<ExchangeRate>> Get()
        {
            List<ExchangeRate>? result = await _httpClient.GetFromJsonAsync<List<ExchangeRate>>("");
            return result != null ? result : new List<ExchangeRate>();
        }
        public async ValueTask<ExchangeRate> Get(int id)
        {
            ExchangeRate? result = await _httpClient.GetFromJsonAsync<ExchangeRate>($"{id}");
            return result != null ? result : new ExchangeRate();
        }
        public async ValueTask<Account> Convert(int id, CurrencyType inType, decimal amount, CurrencyType outType)
        {
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{id}/Convert?inType={inType}&amount={amount}&outType={outType}", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            Account? account = JsonConvert.DeserializeObject<Account>(responseBody);
            return account != null ? account : new Account();
        }
    }

}



