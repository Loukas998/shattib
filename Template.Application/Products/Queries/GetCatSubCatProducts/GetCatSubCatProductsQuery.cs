using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetCatSubCatProducts
{
	public class GetCatSubCatProductsQuery(int categoryId) : IRequest<IEnumerable<CatSubCatProductsDto>>
	{
		public int CategoryId { get; } = categoryId;
	}
}
