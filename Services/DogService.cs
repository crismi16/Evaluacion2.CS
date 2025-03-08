using Evaluacion2.Models;
using Newtonsoft.Json;

namespace Evaluacion2.Services
{
    public class DogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DogService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["DogApiKey"];
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        }

        public async Task<DogListResult> GetDogsAsync(int page = 1, int pageSize = 15)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.thedogapi.com/v1/images/search?page={page}&limit={pageSize}");
                var dogs = JsonConvert.DeserializeObject<List<Dog>>(response);
                return new DogListResult { Data = dogs };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de HTTP: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error de JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return null;
            }
        }

        public async Task<Dog> GetDogDetailsAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.thedogapi.com/v2/breeds/{id}");
                var data = JsonConvert.DeserializeObject<Dog>(response);

                return data;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de HTTP: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error de JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return null;
            }
        }
    }
}
