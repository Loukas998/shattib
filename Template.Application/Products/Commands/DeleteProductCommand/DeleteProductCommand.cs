using MediatR;

namespace Template.Application.Products.Commands.DeleteProductCommand
{
	public class DeleteProductCommand(int productId) : IRequest
	{
		public int ProductId { get; } = productId;
	}
}
