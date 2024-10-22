using Microsoft.AspNetCore.Mvc;

namespace PetworldOficial.MVC.Controllers;

public class CartController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}