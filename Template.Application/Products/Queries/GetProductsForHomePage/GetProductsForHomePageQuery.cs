using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetProductsForHomePage;

public class GetProductsForHomePageQuery : IRequest<IEnumerable<MiniProductDto>>
{
    public int? categoryId { get; set; }
    public int? subcategoryId { get; set; }
    public string? color { get; set; }
    public float minPrice { get; set; } = 0;
    public float? maxPrice { get; set; }
    public string? searchTerm { get; set; }
    public string? sortOrder { get; set; }
}