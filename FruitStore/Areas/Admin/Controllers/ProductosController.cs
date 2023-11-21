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
            AdminAgregarProductosViewModel vm = new();
            vm.Categorias = categoriasRepository.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new CategoriaModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? ""
                });

            
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarProductosViewModel vm)
        {
            //validar

            if(vm.Archivo != null) // Si selecciono un archivo
            {

                //MIME TYPE
                if(vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes JPG");
                }
                if(vm.Archivo.Length< 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500kb");
                }
            }

            if(ModelState.IsValid)  // cuando es valido
            {
                productosRepository.Insert(vm.Producto);
                if (vm.Archivo == null)  // No eligio archivo
                {
                    //1.obtener el id del producto
                    //2. copiar el archivo llamado no disponible jpg y cambiarle el nombre por el id
                }

            }
            return View(vm);
        }
    }
}
