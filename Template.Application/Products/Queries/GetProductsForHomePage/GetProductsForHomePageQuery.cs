using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetProductsForHomePage
{
	public class GetProductsForHomePageQuery : IRequest<IEnumerable<HomePageProductDto>>
	{
	}
}
