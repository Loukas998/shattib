using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.CreateProductCommand.AppendImage
{
	public class AppendImageCommandHandler(IProductRepository productRepository, IFileService fileService, 
		ILogger<AppendImageCommandHandler> logger) : IRequestHandler<AppendImageCommand, ImageDto>
	{
		public async Task<ImageDto> Handle(AppendImageCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("appending new image to product with id: {ProductId}", request.ProductId);

			var product = await productRepository.GetProductByIdAsync(request.ProductId);
			if(product == null)
			{
				throw new NotFoundException(nameof(Product), request.ProductId.ToString());
			}

			var path = fileService.SaveFile(request.NewImage, "Images/Products", [".jpg", ".png", ".jpeg", ".webg", ".JPG", ".PNG", ".jfif", ".pdf"]);
			var imageStoragePath = await productRepository.StoreProductImageAsync(path, request.ProductId);
			return new ImageDto { ImageStoragePath = imageStoragePath };
		}
	}
}
