using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Template.Application.Specifications.Dtos;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.CreateProductCommand
{
	public class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
		IMapper mapper, IProductRepository productRepository, IFileService fileService, 
		ISpecificationRepository specificationRepository) : IRequestHandler<CreateProductCommand, int>
	{
		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new product {@Product}", request);

			var product = mapper.Map<Product>(request);
			
			int id = await productRepository.CreateProductAsync(product);
			

			if (request.Specifications != null)
			{
				List<ProductSpecification> productSpecifications = [];
				foreach (var specification in request.Specifications!)
				{
					logger.LogInformation("Adding specification: {@Specification}", specification);
					int specificationId = await specificationRepository.AddAttribute(new Specification { Name = specification.Name });
					var productSpecification = new ProductSpecification
					{
						ProductId = id,
						SpecificationId = specificationId,
						Value = specification.Value
					};
					productSpecifications.Add(productSpecification);
				}
				await specificationRepository.AddProductAttribute(productSpecifications);
			}
			await productRepository.StoreImagePath(product.Images, id);
			return id;
		}
	}
}
//foreach (var path in paths)
//{
//	product.Images.Add(new ProductImages { ImagePath = path, ProductId = id });
//	await productRepository.SaveChanges();
//}
