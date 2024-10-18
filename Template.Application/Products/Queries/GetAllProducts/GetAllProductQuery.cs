using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetAllProducts
{
	public class GetAllProductQuery : IRequest<IEnumerable<ProductDto>>
	{
	}
}
