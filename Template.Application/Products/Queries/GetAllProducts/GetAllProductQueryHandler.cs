using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Application.Products.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Products.Queries.GetAllProducts
{
	public class GetAllProductQueryHandler(ILogger<GetAllProductQueryHandler> logger,
		IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDto>>
	{
		public async Task<IEnumerable<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all products");
			var products = await productRepository.GetAllAsync();
			var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);
			return productDtos;
		}
	}
}
