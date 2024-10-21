using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class ProductRepository(TemplateDbContext dbContext, IWebHostEnvironment webHostEnvironment) : IProductRepository
	{
		private readonly List<string> allowedExtension = [".jpg", ".jpeg", ".png"];

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
				string extension = Path.GetExtension(image.FileName);
				if(allowedExtension.Contains(extension))
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
		}

		public async Task StoreProductImageAsync(IFormFile image, int entityId)
		{
			string extension = Path.GetExtension(image.FileName);
			if (allowedExtension.Contains(extension))
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

		public async Task DeleteProductImageAsync(ProductImages image)
		{
			dbContext.Remove(image);
			await dbContext.SaveChangesAsync();
			
			string fullPath = Path.Combine(webHostEnvironment.ContentRootPath, image.ImagePath);
			File.Delete(fullPath);
		}

		public async Task<ProductImages?> GetProductImageAsync(int imageId)
		{
			var image = await dbContext.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId);
			return image;
		}

		public async Task Delete(Product entity)
		{
			var productImages = dbContext.ProductImages.Where(i => i.ProductId == entity.Id);

			foreach (var productImage in productImages)
			{
				dbContext.ProductImages.Remove(productImage);

				string fullPath = Path.Combine(webHostEnvironment.ContentRootPath, productImage.ImagePath);
				File.Delete(fullPath);
			}

			dbContext.products.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await dbContext.products.ToListAsync();
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await dbContext.products
				.Include(p => p.Images == null ? null : p.Images)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task SaveChanges() => await dbContext.SaveChangesAsync();

		
	}
}
