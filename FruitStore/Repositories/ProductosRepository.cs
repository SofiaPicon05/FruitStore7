using FruitStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitStore.Repositories
{
    public class ProductosRepository : Repository<Productos>
    {
        public ProductosRepository(FruteriaShopContext ctx) : base(ctx)
        {

        }
        public IEnumerable<Productos>GetProductosByCategoria(string categoria)
        {
            return Ctx.Productos
                .Include(x=>x.IdCategoriaNavigation)
                .Where(x=>x.IdCategoriaNavigation!=null &&
                x.IdCategoriaNavigation.Nombre
                ==categoria)
                .OrderBy(x=>x.Nombre) ;
        }
        public Productos? GetByNombre (string nombre)
        {
            return Ctx.Productos
                .Include(x=> x.IdCategoriaNavigation)
                .FirstOrDefault(x => x.Nombre == nombre);
        }
    }
}
