using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Areas.Admin.Controllers
{
    public class ProductosController : Controller
    {

        [HttpGet]
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
