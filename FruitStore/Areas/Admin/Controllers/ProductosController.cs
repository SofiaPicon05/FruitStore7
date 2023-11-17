using Microsoft.AspNetCore.Mvc;
using FruitStore.Areas.Admin.Models;
using FruitStore.Repositories;
using FruitStore.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FruitStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly ProductosRepository productosRepository;
        private readonly Repository<Categorias> categoriasRepository;

        public ProductosController(ProductosRepository productosRepository,
            Repository<Categorias> categoriasRepository)
        {
            this.productosRepository = productosRepository;
            this.categoriasRepository = categoriasRepository;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index(AdminProductosViewModel vm)
        {
            vm.Categorias=categoriasRepository.GetAll().OrderBy(x=>x.Nombre)
                .Select(x=> new CategoriaModel()
                {
                Id = x.Id,
                Nombre=x.Nombre?? ""
                });
                
            
            if (vm.IdCategoriaSeleccionada == 0) //regresa todo
            {
                vm.Productos = productosRepository.GetAll().OrderBy(x => x.Nombre).
                    Select(x => new ProductoModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre?? "",
                        Categoria = x.IdCategoriaNavigation?.Nombre??""
                    });
                
                  
                
                
            }
            else // de la categoria seleccionada
            {
                vm.Productos = productosRepository.GetProductosByCategoria(vm.IdCategoriaSeleccionada).Select(x => new ProductoModel()
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
        public IActionResult Agregar(AdminAgregarProductosViewModel vm)
        {
            return View();
        }
    }
}
