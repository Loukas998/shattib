using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements
{
	public class CreateSpecifiedMeasurementsCommand : IRequest<int>
	{
		public string Height { get; set; } = default!;
		public string Width { get; set; } = default!;
		public string Details { get; set; } = default!;
		public string? Measurement { get; set; }
		public float Quantity { get; set; } = default!;
		public IFormFile? Image { get; set; }
	}
}
