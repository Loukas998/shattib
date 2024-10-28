using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.CreateProductCommand
{
	public class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
		IMapper mapper, IProductRepository productRepository, ISpecificationRepository specificationRepository) : IRequestHandler<CreateProductCommand, int>
	{
		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new product {@Product}", request);
			var product = mapper.Map<Product>(request);	
			
			if(request.Specifications != null && request.Specifications.Any())
			{
				foreach(var specification in request.Specifications)
				{
					int specificationId;
					var specfromdb = await specificationRepository.GetAttributeByName(specification.Name);

					if (specfromdb != null)
					{
						specificationId = specfromdb.Id;
					}
					else
					{
						specificationId = await specificationRepository.AddAttribute(new Specification { Name = specification.Name});
					}

					var productSpec = new ProductSpecification
					{
						SpecificationId = specificationId,
						Value = specification.Value
					};

					product.ProductSpecifications.Add(productSpec);
				}
			}

			int id = await productRepository.CreateProductAsync(product);
			return id;
		}
	}
}

