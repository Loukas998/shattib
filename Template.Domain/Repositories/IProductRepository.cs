﻿using Microsoft.AspNetCore.Http;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories;

public interface IProductRepository
{
    public Task<int> CreateProductAsync(Product entity);
    public Task<Product?> GetProductByIdAsync(int id);
    public Task<List<ProductSpecificationDto>>? GetProductSpecificationDtos(int id);

    public Task<IEnumerable<Product>> GetAllAsync(int? categoryId, int? subcategoryId, string? color, float minPrice,
        float? maxPrice, string? searchTerm, string? sortOrder);

    public Task SaveChanges();
    public Task Delete(Product entity);
    public Task DeleteProductImageAsync(ProductImages image);
    public Task<ProductImages?> GetProductImageAsync(int imageId);

    public Task<string> StoreProductImageAsync(string fullImagePath, int entityId);
    //don't change anything in the last 3... you will get an error.. leave it to me

    public Task<IEnumerable<Category>> GetCategoriesWithSubWithProductsAsync(int categoryId);
}