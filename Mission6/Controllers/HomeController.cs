using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6.Models;

namespace Mission6.Controllers;

public class HomeController : Controller
{
    
    private Context _context;
    public HomeController(Context someName)
    {
        _context = someName;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Joel()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Movies()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Movies(Application response)
    {
        _context.Applications.Add(response);
        _context.SaveChanges();
        return View("Confirmation", response);
    }

}