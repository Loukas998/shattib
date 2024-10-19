using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetProduct
{
	public class GetProductByIdQuery(int productId) : IRequest<ProductDto>
	{
		public int ProductId { get; } = productId;
	}
}
