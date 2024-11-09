using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Statistics.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfProducts
{
    public class GetNumberOfProductsQueryHandler(ILogger<GetNumberOfProductsQueryHandler> logger, 
		IStatisticsRepository statisticsRepository) : IRequestHandler<GetNumberOfProductsQuery, StatisticsDto>
	{
		public async Task<StatisticsDto> Handle(GetNumberOfProductsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting number of products");

			return new StatisticsDto { Count = await statisticsRepository.GetNumberOfProductsAsync(), Entity = "منتجات" };
		}
	}
}
