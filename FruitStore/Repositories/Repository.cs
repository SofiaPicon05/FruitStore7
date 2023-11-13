using FruitStore.Models.Entities;

namespace FruitStore.Repositories
{
	public class Repository<T> where T : class
	{
		public Repository(FruteriaShopContext ctx)
		{
			Ctx = ctx;
		}

		public FruteriaShopContext Ctx { get; }
		public virtual IEnumerable<T> GetAll()
		{
			return Ctx.Set<T>();
		}
		public virtual T? Get(object id)
		{
			return Ctx.Find<T>(id);
		}
		public virtual void Insert(T entity)
		{
			Ctx.Add(entity);
			Ctx.SaveChanges();
		}
		public virtual void Update(T entity)
		{
			Ctx.Update(entity);
			Ctx.SaveChanges();
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if(entity != null) 
			{
				Delete(entity);
		    }
		}
	}
	
}
