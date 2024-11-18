using MediatR;

namespace Template.Application.SpecifiedMeasurements.Commands.DeleteSpecifiedMeasurements
{
	public class DeleteSpecifiedMeasurementsCommand : IRequest
	{
		public int Id { get; set; }
	}
}
