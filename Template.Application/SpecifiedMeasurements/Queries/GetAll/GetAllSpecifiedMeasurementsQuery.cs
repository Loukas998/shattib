using MediatR;
using Template.Application.SpecifiedMeasurements.Dtos;

namespace Template.Application.SpecifiedMeasurements.Queries.GetAll
{
	public class GetAllSpecifiedMeasurementsQuery : IRequest<IEnumerable<SpecifiedMeasurementDto?>>
	{
	}
}
