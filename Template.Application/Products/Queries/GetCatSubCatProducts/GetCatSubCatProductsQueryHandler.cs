using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Products.Queries.GetCatSubCatProducts
{
	public class GetCatSubCatProductsQueryHandler(ILogger<GetCatSubCatProductsQueryHandler> logger,
		IMapper mapper, IProductRepository productRepository)
		: IRequestHandler<GetCatSubCatProductsQuery, IEnumerable<CatSubCatProductsDto>>
	{
		public async Task<IEnumerable<CatSubCatProductsDto>> Handle(GetCatSubCatProductsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting category with subcategories with products");
			var categories = await productRepository.GetCategoriesWithSubWithProductsAsync(request.CategoryId);
			var result = mapper.Map<IEnumerable<CatSubCatProductsDto>>(categories);
			return result;
		}
	}
}
