using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Statistics.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetProfitsByDate
{
	public class GetProfitsByDateQueryHandler(ILogger<GetProfitsByDateQueryHandler> logger, 
		IStatisticsRepository statisticsRepository) : IRequestHandler<GetProfitsByDateQuery, ProfitsDto>
	{
		public async Task<ProfitsDto> Handle(GetProfitsByDateQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting the profit of date: {Date}", request);
			var miniProfits = await statisticsRepository.GetProfitsByDate(request.Year, request.Month, request.Day);
			float total = miniProfits.Sum(p => p.TotalPrice);
			ProfitsDto profit = new() { MiniProfitsDtos = miniProfits, Total = total };
			return profit;
		}
	}
}
