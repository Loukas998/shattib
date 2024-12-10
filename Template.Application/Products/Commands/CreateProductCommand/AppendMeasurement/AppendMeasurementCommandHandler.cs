using MediatR;

namespace Template.Application.Products.Commands.CreateProductCommand.AppendMeasurement
{
	public class AppendMeasurementCommandHandler : IRequestHandler<AppendMeasurementCommand>
	{
		public Task Handle(AppendMeasurementCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
