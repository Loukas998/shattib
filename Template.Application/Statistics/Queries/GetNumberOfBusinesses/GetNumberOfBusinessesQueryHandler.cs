using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Statistics.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfBusinesses
{
    public class GetNumberOfBusinessesQueryHandler(ILogger<GetNumberOfBusinessesQueryHandler> logger, 
		IStatisticsRepository statisticsRepository) : IRequestHandler<GetNumberOfBusinessesQuery, StatisticsDto>
	{
		public async Task<StatisticsDto> Handle(GetNumberOfBusinessesQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting number of businesses");

			return new StatisticsDto { Count = await statisticsRepository.GetNumberOfBusinessesAsync(), Entity = "شركات" };

		}
	}
}
