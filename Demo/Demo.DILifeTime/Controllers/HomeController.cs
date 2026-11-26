using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo.DILifeTime.Models;
using Demo.DILifeTime.Services;

namespace Demo.DILifeTime.Controllers;

public class HomeController(ISingleton singleton, IScoped scoped, ITransient transient, GuidService guidService) : Controller
{
    public IActionResult Index()
    {
        ViewBag.Singleton = singleton.Guid.ToString();
        ViewBag.Scoped = scoped.Guid.ToString();
        ViewBag.Transient = scoped.Guid.ToString();

        ViewBag.ServiceSingleton = guidService.Singleton.Guid.ToString();
        ViewBag.ServiceScoped = guidService.Scoped.Guid.ToString();
        ViewBag.ServiceTransient = guidService.Transient.Guid.ToString();
        return View();
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
