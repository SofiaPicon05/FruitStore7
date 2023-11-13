using FruitStore.Models.Entities;
using FruitStore.Models.ViewModels;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Controllers
{
    public class HomeController : Controller
    {
        public ProductosRepository repository;

        public HomeController(ProductosRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos(string Id)  // id es el nombre de la categoria
        {
            Id = Id.Replace("-", " ");

            ProductosViewModel vm = new()
            {
                Categoria = Id,
                Productos=repository
                .GetProductosByCategoria(Id)
                
                
                .Select(x=> new ProductosModel
                {
                    Id=x.Id,
                    Nombre=x.Nombre?? "",
                    Precio=x.Precio?? 0m
                })
            };
            return View(vm);
        }

        public IActionResult Ver(string Id)
        {
            Id=Id.Replace("-", " ");
            var producto = repository.GetByNombre(Id);
            if (producto == null)
            {
                return RedirectToAction("Index");
            }
            VerProductoViewModel vm = new()
            {
                Id = producto.Id,
                Categoria = producto.IdCategoriaNavigation?.Nombre ?? "",
                Descripcion = producto.Descripcion ?? "",
                Precio = producto.Precio ?? 0,
                UnidadMedida = producto.UnidadMedida ?? "",
                Nombre = producto.Nombre ?? ""

            };
            return View(vm);
        }

    }
}
