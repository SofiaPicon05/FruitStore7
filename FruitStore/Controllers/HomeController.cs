using FruitStore.Helpers;
using FruitStore.Models.Entities;
using FruitStore.Models.ViewModels;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace FruitStore.Controllers
{
    public class HomeController : Controller
    {
        public ProductosRepository repository;
        private readonly Repository<Usuarios> userRepository;

        public HomeController(ProductosRepository repository, Repository<Usuarios> userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
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
                    Precio=x.Precio?? 0m,
                    FechaModificacion
                    =new FileInfo($"wwwroot/img_frutas/{x.Id}.jpg")  // text onyl para cuando se hacen los cambios de las imagenes
                    .LastWriteTime.ToString("yyyyMMddhhmm")

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


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Correo))
            {
                ModelState.AddModelError("", "Escriba el correo electronico del usuario.");
            }

            if (string.IsNullOrWhiteSpace(vm.Contraseña))
            {
                ModelState.AddModelError("", "Escriba la contraseña  del usuario.");
            }
            if (ModelState.IsValid)
            {
             var user =userRepository.GetAll().FirstOrDefault(x=>x.CorreoElectronico == vm.Correo
             && x.Contrasena == Encriptacion.StringToSHA512(vm.Contraseña));   
            if (user == null) 
                {
                    ModelState.AddModelError("", "Escriba la contra correcta");
                }
                else
                //loguear
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Nombre));
                    claims.Add(new Claim(ClaimTypes.Role, user.Rol == 1 ? "Administrador" : "Supervisor"));

                    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(new ClaimsPrincipal(identity),
                        new AuthenticationProperties
                        {
                            IsPersistent=true,
                        });

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
           
            return View(vm);

            

            
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Denied()
        {
            return View();
        }

    }
}
