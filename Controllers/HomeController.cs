using Evaluacion2.Models;
using Evaluacion2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Evaluacion2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DogService _dogService;

        public HomeController(ILogger<HomeController> logger, DogService dogService)
        {
            _logger = logger;
            _dogService = dogService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                var breedResponse = await _dogService.GetBreedsAsync(page);

                if (breedResponse == null)
                {
                    _logger.LogWarning("La respuesta de la API de razas fue nula.");
                    return View(new BreedResponse { Data = new System.Collections.Generic.List<Breed>() });
                }

                foreach (var breed in breedResponse.Data)
                {
                    breed.ImageUrl = await _dogService.GetImageUrlAsync(breed.Attributes.Name);
                }

                return View(breedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de razas.");
                return View(new BreedResponse { Data = new System.Collections.Generic.List<Breed>() });
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var breed = await _dogService.GetBreedDetailsAsync(id);

                if (breed == null)
                {
                    _logger.LogWarning($"No se encontraron detalles para la raza con ID: {id}.");
                    return View(new Breed());
                }

                breed.ImageUrl = await _dogService.GetImageUrlAsync(breed.Attributes.Name);

                return View(breed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de la raza con ID: {id}.");
                return View(new Breed());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}