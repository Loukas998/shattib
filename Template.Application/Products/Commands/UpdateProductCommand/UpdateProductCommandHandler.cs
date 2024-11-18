using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.UpdateProductCommand
{
	public class UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger,
		IMapper mapper, IProductRepository productRepository, IFileService fileService) : IRequestHandler<UpdateProductCommand>
	{
		public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating product with id: {ProductId}, the new product: {@Product}",
				request.ProductId, request);

			var product = await productRepository.GetProductByIdAsync(request.ProductId);
			if (product == null)
			{
				throw new NotFoundException(nameof(Product), request.ProductId.ToString());
			}

			foreach(var image in product.Images)
			{
				string fileName = Path.GetFileName(image.ImagePath);
				string fullPath = Path.Combine("Images", "Products", fileName).Replace("\\", "/");
				fileService.DeleteFile(fullPath);
			}

			mapper.Map(request, product);
			await productRepository.SaveChanges();
		}
	}
}
