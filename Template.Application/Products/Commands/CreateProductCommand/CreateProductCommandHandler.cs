using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.CreateProductCommand
{
	public class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
		IMapper mapper, IProductRepository productRepository) : IRequestHandler<CreateProductCommand, int>
	{
		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new product {@Product}", request);
			var product = mapper.Map<Product>(request);
			return await productRepository.CreateProductAsync(product);
		}
	}
}
