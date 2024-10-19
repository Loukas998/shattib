using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class ProductRepository(TemplateDbContext dbContext) : IProductRepository
	{
		public async Task<int> CreateProductAsync(Product entity)
		{
			dbContext.products.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Product entity)
		{
			dbContext.products.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await dbContext.products.ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await dbContext.products.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task SaveChanges() => await dbContext.SaveChangesAsync();
	}
}
