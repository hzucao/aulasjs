using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InduMovel.Models;
using InduMovel.ViewModel;
using InduMovel.Repositories.Interfaces;

namespace InduMovel.Controllers;

public class HomeController : Controller
{
   private readonly IMovelRepository _movelRepository;

    public HomeController(IMovelRepository movelRepository)
    {
        _movelRepository = movelRepository;
    }

    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel{
            MoveisEmProducao = _movelRepository.MoveisEmProducao
        };
        return View(homeViewModel);
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
