using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public Task<int> CreateProductAsync(Product entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Product entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetProductByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
