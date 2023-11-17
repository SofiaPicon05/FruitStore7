using FruitStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitStore.Repositories
{
    public class ProductosRepository : Repository<Productos>
    {
        public ProductosRepository(FruteriaShopContext ctx) : base(ctx)
        {

        }

        public override IEnumerable<Productos> GetAll()
        {
            return Ctx.Productos
                .Include(x => x.IdCategoriaNavigation).OrderBy(x => x.Nombre);
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
        public IEnumerable<Productos> GetProductosByCategoria(int categoria)
        {
            return Ctx.Productos
                .Include(x => x.IdCategoriaNavigation)
                .Where(x => x.IdCategoriaNavigation != null &&
                x.IdCategoriaNavigation.Id
                == categoria)
                .OrderBy(x => x.Nombre);
        }


        public Productos? GetByNombre (string nombre)
        {
            return Ctx.Productos
                .Include(x=> x.IdCategoriaNavigation)
                .FirstOrDefault(x => x.Nombre == nombre);
        }
    }
}
