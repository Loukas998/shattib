using MediatR;
using Template.Application.Statistics.Dtos;

namespace Template.Application.Statistics.Queries.GetProfitsByDate
{
	public class GetProfitsByDateQuery : IRequest<ProfitsDto>
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
	}
}
