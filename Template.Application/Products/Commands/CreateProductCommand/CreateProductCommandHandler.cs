using MediatR;

namespace Template.Application.Products.Commands.CreateProductCommand
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
	{
		public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
