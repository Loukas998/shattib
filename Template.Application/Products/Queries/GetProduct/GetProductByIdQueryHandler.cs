using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Dtos;
using Template.Application.Products.Queries.GetAllProducts;
using Template.Domain.Repositories;

namespace Template.Application.Products.Queries.GetProduct
{
	public class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger,
		IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductDto>
	{
		public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Finding product with id: {ProductId}", request.ProductId);
			var product = await productRepository.GetProductByIdAsync(request.ProductId);
			if (product == null)
			{
				throw new NotImplementedException();
			}
			var result = mapper.Map<ProductDto>(product);
			return result;
		}
	}
}
