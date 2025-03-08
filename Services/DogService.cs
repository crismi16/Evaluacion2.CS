using Evaluacion2.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Evaluacion2.Services
{
    public class DogService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DogService> _logger;
        private readonly string _baseUrl = "https://dogapi.dog/api/v2/breeds";

        public DogService(HttpClient httpClient, ILogger<DogService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<BreedResponse> GetBreedsAsync(int page = 1)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}?page[number]={page}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var breedResponse = JsonConvert.DeserializeObject<BreedResponse>(content);

                return breedResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de solicitud HTTP.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error de deserialización JSON.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado.");
                return null;
            }
        }

        public async Task<Breed> GetBreedDetailsAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Respuesta de la API para {id}: {content}");

                var breedDetailResponse = JsonConvert.DeserializeObject<BreedDetailResponse>(content);
                _logger.LogInformation($"Objeto breedDetailResponse: {JsonConvert.SerializeObject(breedDetailResponse)}");

                return breedDetailResponse?.Data;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de solicitud HTTP.");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error de deserialización JSON.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado.");
                return null;
            }
        }

        public async Task<string> GetImageUrlAsync(string breedName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.thedogapi.com/v1/images/search?q={breedName}&limit=1");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var images = JsonConvert.DeserializeObject<ImageResponse[]>(content);

                return images?.Length > 0 ? images[0].Url : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la URL de la imagen para {breedName}.");
                return null;
            }
        }

        private class ImageResponse
        {
            public string Url { get; set; }
        }
    }
}