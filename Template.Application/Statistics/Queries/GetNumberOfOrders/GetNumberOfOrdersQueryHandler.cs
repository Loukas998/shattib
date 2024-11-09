using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Statistics.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfOrders
{
    public class GetNumberOfOrdersQueryHandler(ILogger<GetNumberOfOrdersQueryHandler> logger,
		IStatisticsRepository statisticsRepository) : IRequestHandler<GetNumberOfOrdersQuery, StatisticsDto>
	{
		public async Task<StatisticsDto> Handle(GetNumberOfOrdersQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting number of orders");

			return new StatisticsDto { Count = await statisticsRepository.GetNumberOfOrdersAsync(), Entity = "طلبات" };

		}
	}
}
