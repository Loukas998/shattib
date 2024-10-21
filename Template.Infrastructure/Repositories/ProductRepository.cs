using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class ProductRepository(TemplateDbContext dbContext, IWebHostEnvironment webHostEnvironment) : IProductRepository
	{
		public async Task<int> CreateProductAsync(Product entity)
		{
			dbContext.products.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task StoreProductImagesAsync(List<IFormFile> images, int entityId)
		{
			foreach(var image in images)
			{
				string fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
				string filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/Products", fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await image.CopyToAsync(stream);
				}

				string fullImagePath = Path.Combine("Images/Products", fileName);

				ProductImages productImage = new ProductImages
				{
					ProductId = entityId,
					ImagePath = fullImagePath
				};

				dbContext.ProductImages.Add(productImage);
				await dbContext.SaveChangesAsync();
			}
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
