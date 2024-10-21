using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Products.Commands.DeleteImagesCommand.DeleteImagesCommand
{
	public class DeleteProductImageCommand(int imageId) : IRequest
	{
		public int ImageId { get; } = imageId;
	}
}
