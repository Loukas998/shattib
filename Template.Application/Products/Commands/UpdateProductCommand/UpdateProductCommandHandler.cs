using MediatR;

namespace Template.Application.Products.Commands.UpdateProductCommand
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
	{
		public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
