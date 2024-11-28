using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class MealService
{
    private readonly HttpClient _httpClient;

    public MealService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/");
    }

    public async Task<string> SearchMealByName(string name)
    {
        var response = await _httpClient.GetAsync($"search.php?s={name}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetMealDetails(string id)
    {
        var response = await _httpClient.GetAsync($"lookup.php?i={id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetRandomMeal()
    {
        var response = await _httpClient.GetAsync("random.php");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetCategoriesAreasIngredients()
    {
        var categories = await _httpClient.GetAsync("list.php?c=list");
        var areas = await _httpClient.GetAsync("list.php?a=list");
        var ingredients = await _httpClient.GetAsync("list.php?i=list");

        return JsonSerializer.Serialize(new
        {
            Categories = await categories.Content.ReadAsStringAsync(),
            Areas = await areas.Content.ReadAsStringAsync(),
            Ingredients = await ingredients.Content.ReadAsStringAsync()
        });
    }

    public async Task<string> FilterByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"filter.php?c={category}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> FilterByArea(string area)
    {
        var response = await _httpClient.GetAsync($"filter.php?a={area}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
