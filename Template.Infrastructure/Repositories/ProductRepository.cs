using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(TemplateDbContext dbContext, IWebHostEnvironment webHostEnvironment) : IProductRepository
{
    private readonly List<string> allowedExtension = [".jpg", ".jpeg", ".png"];

    public async Task<int> CreateProductAsync(Product entity)
    {
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();
		return entity.Id;
    }

    public async Task StoreProductImagesAsync(List<IFormFile> images, int entityId)
    {
        foreach (var image in images)
        {
            var extension = Path.GetExtension(image.FileName);
            if (allowedExtension.Contains(extension))
            {
                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
                var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/Products", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var fullImagePath = Path.Combine("Images/Products", fileName);

                var productImage = new ProductImages
                {
                    ProductId = entityId,
                    ImagePath = fullImagePath
                };

                dbContext.ProductImages.Add(productImage);
                await dbContext.SaveChangesAsync();
            }
        }
    }

	public async Task StoreImagePath(List<ProductImages> productImages, int productId)
	{
        foreach(var productImage in productImages)
        {
			productImage.ProductId = productId;
		}
		dbContext.ProductImages.AddRange(productImages);
		await dbContext.SaveChangesAsync();
	}

    public async Task StoreProductImageAsync(IFormFile image, int entityId)
    {
        var extension = Path.GetExtension(image.FileName);
        if (allowedExtension.Contains(extension))
        {
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/Products", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var fullImagePath = Path.Combine("Images/Products", fileName);

            var productImage = new ProductImages
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

        var fullPath = Path.Combine(webHostEnvironment.ContentRootPath, image.ImagePath);
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

            var fullPath = Path.Combine(webHostEnvironment.ContentRootPath, productImage.ImagePath);
            File.Delete(fullPath);
        }

        dbContext.Products.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        //var productImages = dbContext.ProductImages.Where(pi => pi.ProductId == id);
        return await dbContext.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}