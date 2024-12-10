using MediatR;

namespace Template.Application.Products.Commands.CreateProductCommand.AppendMeasurement
{
	public class AppendMeasurementCommand : IRequest
	{
		public string Name { get; set; } = default!;
		public float Price { get; set; }
	}
}
