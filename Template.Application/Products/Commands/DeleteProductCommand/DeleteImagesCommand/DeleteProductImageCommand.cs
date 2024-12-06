using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Products.Queries.GetProduct;

namespace Template.Application.Products.Commands.DeleteImagesCommand.DeleteImagesCommand
{
	public class DeleteProductImageCommand(int imageId, int productId) : IRequest
	{
		public int ProductId { get; } = productId;
		public int ImageId { get; } = imageId;
	}
}
