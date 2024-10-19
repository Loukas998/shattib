using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.DeleteProductCommand
{
	public class DeleteProductCommandHanlder(ILogger<DeleteProductCommandHanlder> logger,
		IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
	{
		public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting product with id: {ProductId}", request.ProductId);
			var product = await productRepository.GetProductByIdAsync(request.ProductId);
			if (product == null)
			{
				throw new NotImplementedException();
			}
			await productRepository.Delete(product);
		}
	}
}
