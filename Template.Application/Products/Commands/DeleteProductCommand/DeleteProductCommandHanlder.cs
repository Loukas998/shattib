using MediatR;

namespace Template.Application.Products.Commands.DeleteProductCommand
{
	public class DeleteProductCommandHanlder : IRequestHandler<DeleteProductCommand>
	{
		public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
