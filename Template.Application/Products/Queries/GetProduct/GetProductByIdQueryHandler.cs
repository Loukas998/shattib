using MediatR;
using Template.Application.Products.Dtos;

namespace Template.Application.Products.Queries.GetProduct
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
	{
		public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
