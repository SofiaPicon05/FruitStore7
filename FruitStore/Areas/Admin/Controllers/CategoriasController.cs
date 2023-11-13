using FruitStore.Areas.Admin.Models;
using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FruitStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        public CategoriasController(Repositories.Repository<Categorias> repository)
        {
            Repository = repository;
        }

        public Repository<Categorias> Repository { get; }

        public IActionResult Index()
        {
            AdminCategoriasViewModel vm = new();
            {
                vm.Categorias = Repository.GetAll().OrderBy(x => x.Nombre).Select(x => new CategoriaModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? ""
                });
            }
            
            return View(vm);
        }


        public IActionResult Agregar()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
            {
                ModelState.AddModelError("", "Escriba el Nombre de la categoria");
                
            }
            if (ModelState.IsValid)
            {
                Repository.Insert(c);
                return RedirectToAction("Index");  // no se agg categorias o admin, pq es el mismo, el controller sabe q es el same
            }

            return View(c);
        }
    }
}
