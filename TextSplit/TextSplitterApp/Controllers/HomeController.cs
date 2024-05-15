using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TextSplitterApp.Models;

namespace TextSplitterApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(TextViewModel model) => View(model);

        [HttpPost]

        public IActionResult Split(TextViewModel model)
        {
            var splitTextArray = model
                .Text
                .Split(" ",StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            model.SplitText = string.Join(Environment.NewLine, splitTextArray);

            return RedirectToAction("Index", model);
        }
    }
}
