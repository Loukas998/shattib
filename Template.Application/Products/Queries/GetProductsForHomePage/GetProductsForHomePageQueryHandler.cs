using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Products.Queries.GetProductsForHomePage;

public class GetProductsForHomePageQueryHandler(
    ILogger<GetProductsForHomePageQueryHandler> logger,
    IMapper mapper,
    IProductRepository productRepository)
    : IRequestHandler<GetProductsForHomePageQuery, IEnumerable<MiniProductDto>>
{
    public async Task<IEnumerable<MiniProductDto>> Handle(GetProductsForHomePageQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting minified version of products to home page");

        var products = await productRepository.GetAllAsync(request.categoryId,
            request.subcategoryId,
            request.color,
            request.minPrice,
            request.maxPrice,
            request.searchTerm,
            request.sortOrder);

        var results = products.Select(product => new MiniProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            MainImagePath = product.Images.FirstOrDefault()?.ImagePath ?? string.Empty,
            WarehouseCode = product.WarehouseCode,
            SubCategoryName = product.SubCategory.Name
        }).ToList();

        return results;
    }
}