using MediatR;
using Template.Application.Statistics.Dtos;

namespace Template.Application.Statistics.Queries.GetNumberOfOrders
{
    public class GetNumberOfOrdersQuery : IRequest<StatisticsDto>
	{
	}
}
