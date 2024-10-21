using Microsoft.AspNetCore.Http;
using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface IProductRepository
	{
		public Task<int> CreateProductAsync(Product entity);
		public Task<Product> GetProductByIdAsync(int id);
		public Task<IEnumerable<Product>> GetAllAsync();
		public Task SaveChanges();
		public Task Delete(Product entity);
		public Task StoreProductImagesAsync(List<IFormFile> images, int entityId);
	}
}
