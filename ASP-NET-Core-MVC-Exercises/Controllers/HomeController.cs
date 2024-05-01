using Microsoft.AspNetCore.Mvc;
using MVCIntroDemo.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello World!";
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.NET Core MVC app.";
            return View();
        }
        public IActionResult Numbers()
        {
            return View();
        }
        public IActionResult NumbersToN(int count = 3)
        {
            ViewBag.Count = count;
            return View();
        }
    }
}
