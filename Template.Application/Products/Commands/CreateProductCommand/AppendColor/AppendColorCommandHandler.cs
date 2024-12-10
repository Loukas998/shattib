using MediatR;

namespace Template.Application.Products.Commands.CreateProductCommand.AppendColor
{
	public class AppendColorCommandHandler : IRequestHandler<AppendColorCommand>
	{
		public Task Handle(AppendColorCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
