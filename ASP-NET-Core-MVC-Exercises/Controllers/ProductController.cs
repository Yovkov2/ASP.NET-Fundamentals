using Microsoft.AspNetCore.Mvc;
using MVCIntroDemo.Views.Home;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;

namespace MVCIntroDemo.Controllers
{
    public class ProductController : Controller
    {
        
        private IEnumerable<ProductViewModel> _products =
            new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00
                },
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Ham",
                    Price = 5.50
                },
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Bread",
                    Price = 1.50
                }
            };
        public IActionResult All()
        {
            return View(_products);
        }
        public IActionResult ById(int id)
        {
            var product = _products
                .FirstOrDefault(p=> p.Id == id);
            if(product == null)
            {
                return BadRequest();
            }
            return View(product);
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(_products, options);
        }
    }
}
