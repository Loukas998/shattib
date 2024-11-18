using MediatR;
using Template.Application.SpecifiedMeasurements.Dtos;
using Template.Domain.Entities.Orders;

namespace Template.Application.SpecifiedMeasurements.Queries.GetById
{
	public class GetSpecifiedMeasurementByIdQuery(int id) : IRequest<SpecifiedMeasurementDto?>
	{
		public int Id { get; } = id;
	}
}
