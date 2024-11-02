using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(
    TemplateDbContext dbContext,
    IWebHostEnvironment webHostEnvironment,
    IFileService fileService) : IProductRepository
{
    //private readonly List<string> allowedExtension = [".jpg", ".jpeg", ".png"];
    public async Task<int> CreateProductAsync(Product entity)
    {
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task StoreProductImageAsync(IFormFile image, int entityId)
    {
        var fullImagePath = fileService.SaveFile(image, "Images/Products", [".jpg", ".jpeg", ".png"]);

        var productImage = new ProductImages
        {
            ProductId = entityId,
            ImagePath = fullImagePath
        };

        dbContext.ProductImages.Add(productImage);
        await dbContext.SaveChangesAsync();
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
		return await dbContext.Products.Include(p => p.Images).ToListAsync();
	}

	public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products
            .Include(p => p.ProductSpecifications)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ProductSpecificationDto>>? GetProductSpecificationDtos(int id)
    {
        var prodSpec = from ps in dbContext.Productspecifications
            join s in dbContext.Specifications
                on ps.SpecificationId equals s.Id
            join p in dbContext.Products
                on ps.ProductId equals id
            where p.Id == id
            select new ProductSpecificationDto
            {
                Name = s.Name,
                Value = ps.Value
            };
        return await prodSpec.ToListAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}