using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.DeleteImagesCommand.DeleteImagesCommand
{
	public class DeleteProductImageCommandHandler(ILogger<DeleteProductImageCommandHandler> logger,
		IProductRepository productRepository) : IRequestHandler<DeleteProductImageCommand>
	{
		public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting product\'s image with id: {ImageId}", request.ImageId);

			var image = await productRepository.GetProductImageAsync(request.ImageId);
			if (image != null)
			{
				await productRepository.DeleteProductImageAsync(image);
			}
		}
	}
}
