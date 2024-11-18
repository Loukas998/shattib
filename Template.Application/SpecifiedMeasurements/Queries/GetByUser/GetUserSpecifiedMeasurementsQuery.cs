using MediatR;
using Template.Application.SpecifiedMeasurements.Dtos;

namespace Template.Application.SpecifiedMeasurements.Queries.GetByUser
{
	public class GetUserSpecifiedMeasurementsQuery : IRequest<IEnumerable<SpecifiedMeasurementDto>>
	{
	}
}
