using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetAllProducts
{
	public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDto>>
	{
		public Task<IEnumerable<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
