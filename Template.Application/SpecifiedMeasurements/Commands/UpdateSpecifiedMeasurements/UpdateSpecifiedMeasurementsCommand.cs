using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.SpecifiedMeasurements.Commands.UpdateSpecifiedMeasurements
{
	public class UpdateSpecifiedMeasurementsCommand : IRequest
	{
		public int Id { get; set; }
		public string Height { get; set; } = default!;
		public string Width { get; set; } = default!;
		public string Details { get; set; } = default!;
		public string? Measurement { get; set; }
		public IFormFile? Image { get; set; }
	}

}
