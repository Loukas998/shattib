using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Statistics.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfClients
{
    public class GetNumberOfClientsQueryHandler(ILogger<GetNumberOfClientsQueryHandler> logger, 
		IStatisticsRepository statisticsRepository) : IRequestHandler<GetNumberOfClientsQuery, StatisticsDto>
	{
		public async Task<StatisticsDto> Handle(GetNumberOfClientsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting number of clients");

			return new StatisticsDto { Count = await statisticsRepository.GetNumberOfProductsAsync(), Entity = "زبائن" };

		}
	}
}
