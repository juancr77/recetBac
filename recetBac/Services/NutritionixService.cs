using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecetasAPI.Services
{
    public class NutritionixService
    {
        private readonly HttpClient _httpClient;

        public NutritionixService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-app-id", "2d9a0737");
            _httpClient.DefaultRequestHeaders.Add("x-app-key", "969a3abdd56ff4d221575eb1d350baf0");
        }

        // 1. Buscar alimentos por nombre
        public async Task<string> SearchFoodAsync(string query)
        {
            var response = await _httpClient.GetAsync($"https://trackapi.nutritionix.com/v2/search/instant?query={query}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }

        // 2. Obtener información detallada de un alimento
        public async Task<string> GetFoodDetailsAsync(string query)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { query }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://trackapi.nutritionix.com/v2/natural/nutrients", content);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }

        // 3. Buscar alimentos por código UPC
        public async Task<string> SearchByUPCAsync(string upc)
        {
            var response = await _httpClient.GetAsync($"https://trackapi.nutritionix.com/v2/search/item?upc={upc}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }
    }
}
