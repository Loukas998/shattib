using MediatR;
using Template.Application.Statistics.Dtos;

namespace Template.Application.Statistics.Queries.GetNumberOfClients
{
    public class GetNumberOfClientsQuery : IRequest<StatisticsDto>
	{
	}
}
