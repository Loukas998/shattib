using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.UpdateProductCommand.UpdateImagesCommand
{
	public class UpdateProductImageCommandHandler(ILogger<UpdateProductImageCommandHandler> logger,
		IProductRepository productRepository) : IRequestHandler<UpdateProductImageCommand>
	{
		public async Task Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating product\'s image with id: {ImageId}", request.OldImageId);

			var image = await productRepository.GetProductImageAsync(request.OldImageId);
			if (image == null) 
			{
				throw new NotFoundException(nameof(ProductImages), request.OldImageId.ToString());
			}
			await productRepository.DeleteProductImageAsync(image);

			if (request.NewImage != null)
			{
				await productRepository.StoreProductImageAsync(request.NewImage, request.ProductId);
			}
		}
	}
}
