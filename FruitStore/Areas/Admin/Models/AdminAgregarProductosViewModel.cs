using FruitStore.Models.Entities;

namespace FruitStore.Areas.Admin.Models
{
    public class AdminAgregarProductosViewModel
    {
        public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
        public Productos Producto { get; set; } = null!;
        public IFormFile Archivo { get; set; } = null!;
    }
}
