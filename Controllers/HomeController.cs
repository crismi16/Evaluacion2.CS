using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Evaluacion2.Models;
using Evaluacion2.Services;

namespace Evaluacion2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DogService _dogService;

    public HomeController(ILogger<HomeController> logger, DogService dogService)
    {
        _logger = logger;
        _dogService = dogService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 15)
    {
        var dogResult = await _dogService.GetDogsAsync(page, pageSize);
        var dogs = dogResult.Data;

        // Actualizar el modelo con información de paginación
        foreach (var dog in dogs)
        {
            dog.TotalDogs = dogResult.Count;
            dog.PageSize = pageSize;
            dog.CurrentPage = page;
        }

        return View(dogs);
    }

    public async Task<IActionResult> Details(string id)
    {
        var dog = await _dogService.GetDogDetailsAsync(id);
        return View(dog);
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
