using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProductRepository(TemplateDbContext dbContext) : IProductRepository
{
    // private readonly List<string> allowedExtension = [".jpg", ".jpeg", ".png"];
    public async Task<int> CreateProductAsync(Product entity)
    {
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<string> StoreProductImageAsync(string fullImagePath, int entityId)
    {

        var productImage = new ProductImages
        {
            ProductId = entityId,
            ImagePath = fullImagePath
        };

        dbContext.ProductImages.Add(productImage);
        await dbContext.SaveChangesAsync();
        return productImage.ImagePath;
    }

    public async Task DeleteProductImageAsync(ProductImages image)
    {
        dbContext.Remove(image);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ProductImages?> GetProductImageAsync(int imageId)
    {
        var image = await dbContext.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId);
        return image;
    }

    public async Task Delete(Product entity)
    {
        dbContext.Products.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int? categoryId, int? subcategoryId, string? color,
        float minPrice,
        float? maxPrice, string? searchTerm, string? sortOrder)
    {
        // return await dbContext.Products.Include(p => p.Images).ToListAsync();
        var query = dbContext.Products
            .Include(p => p.Images)
            .Include(p => p.SubCategory)
            .ThenInclude(sb => sb.Category)
            .AsQueryable();

		if (!string.IsNullOrEmpty(searchTerm))
			query = query.Where(p =>
				p.Name.Contains(searchTerm) ||
				p.Description.Contains(searchTerm) ||
				p.WarehouseCode.Contains(searchTerm) || p.SubCategory.Name.Contains(searchTerm) ||
				p.SubCategory.Category.Name.Contains(searchTerm));

		if (categoryId > 0)
            query = query.Where(p => p.SubCategory.CategoryId == categoryId);

        if (subcategoryId > 0)
            query = query.Where(p => p.SubCategoryId == subcategoryId);

        query = query.Where(p => p.Price >= minPrice);

        if (maxPrice > 0)
            query = query.Where(p => p.Price <= maxPrice);

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(p =>
                p.Name.Contains(searchTerm) ||
                p.Description.Contains(searchTerm) ||
                p.WarehouseCode.Contains(searchTerm));

        // Apply sorting
        query = sortOrder switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Id)
        };

        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products
            .Include(p => p.ProductSpecifications)
            .Include(p => p.Colors)
            .Include(p => p.Measurements)
            .Include(p => p.Images)
            .Include(p => p.SubCategory)
            .ThenInclude(sb => sb.Category)
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

	public async Task<IEnumerable<Category>> GetCategoriesWithSubWithProductsAsync(int categoryId)
	{
        var categories = dbContext.Categories.AsQueryable();
        if(categoryId != 0)
        {
           categories = categories.Where(c => c.Id == categoryId);
        }

        return await categories.Include(c => c.SubCategories)
                                .ThenInclude(sb => sb.Products).ToListAsync();
	}
}