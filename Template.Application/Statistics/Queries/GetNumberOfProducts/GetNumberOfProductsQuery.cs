using MediatR;
using Template.Application.Statistics.Dtos;

namespace Template.Application.Statistics.Queries.GetNumberOfProducts
{
    public class GetNumberOfProductsQuery : IRequest<StatisticsDto>
	{
	}
}
