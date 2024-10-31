using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Products.Queries.GetProductsForHomePage
{
	public class GetProductsForHomePageQueryHandler(ILogger<GetProductsForHomePageQueryHandler> logger, IMapper mapper,
		IProductRepository productRepository)
		: IRequestHandler<GetProductsForHomePageQuery, IEnumerable<HomePageProductDto>>
	{
		public async Task<IEnumerable<HomePageProductDto>> Handle(GetProductsForHomePageQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting minified version of products to home page");
			var products = await productRepository.GetAllAsync();
			var results = mapper.Map<IEnumerable<HomePageProductDto>>(products);
			return results;
		}
	}
}
