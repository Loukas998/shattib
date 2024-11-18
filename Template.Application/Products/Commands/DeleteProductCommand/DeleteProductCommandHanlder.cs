using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.DeleteProductCommand
{
	public class DeleteProductCommandHanlder(ILogger<DeleteProductCommandHanlder> logger,
		IProductRepository productRepository, IFileService fileService) : IRequestHandler<DeleteProductCommand>
	{
		public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting product with id: {ProductId}", request.ProductId);
			var product = await productRepository.GetProductByIdAsync(request.ProductId);
			if (product == null)
			{
				throw new NotFoundException(nameof(Product), request.ProductId.ToString());
			}

			foreach (var image in product.Images)
			{
				string fileName = Path.GetFileName(image.ImagePath);
				string fullPath = Path.Combine("Images", "Products", fileName).Replace("\\", "/");
				fileService.DeleteFile(fullPath);
			}

			await productRepository.Delete(product);
		}
	}
}
