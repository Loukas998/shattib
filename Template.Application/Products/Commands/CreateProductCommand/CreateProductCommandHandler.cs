using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommandHandler(
    ILogger<CreateProductCommandHandler> logger,
    IMapper mapper,
    IProductRepository productRepository,
    ISpecificationRepository specificationRepository,
	IFileService fileService) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new product {@Product}", request);
        var product = mapper.Map<Product>(request);

        if (request.Specifications != null && request.Specifications.Count != 0)
        {
			foreach (var specification in request.Specifications)
			{
				int specificationId;
				var specfromdb = await specificationRepository.GetAttributeByName(specification.Name);

				if (specfromdb != null)
					specificationId = specfromdb.Id;
				else
					specificationId =
						await specificationRepository.AddAttribute(new Specification { Name = specification.Name });

				var productSpec = new ProductSpecification
				{
					SpecificationId = specificationId,
					Value = specification.Value
				};

				product.ProductSpecifications.Add(productSpec);
			}
		}

		foreach(var color in request.ProductColors)
		{
			var productColor = new Color
			{
				HexCode = color.HexCode,
				Price = color.Price,
				ImagePath = fileService.SaveFile(color.ImagePath, "Images/Products/Colors", [".jpg", ".png", ".JPG", ".PNG", ".jpeg", ".JPEG", ".pdf"])
			};
			product.Colors.Add(productColor);
		}

		foreach(var measurement in request.Measurements)
		{
			var measure = new Measurement
			{
				Name = measurement.Name,
				Price = measurement.Price,
			};
			product.Measurements.Add(measure);
		}

        var id = await productRepository.CreateProductAsync(product);
        return id;
    }
}