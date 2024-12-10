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
		IMapper mapper, IProductRepository productRepository, IFileService fileService,
		ISpecificationRepository specificationRepository) : IRequestHandler<UpdateProductCommand>
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
			mapper.Map(request, product);
			await productRepository.SaveChanges();
		}
	}
}

//if (request.Measurements != null)
//{
//	product.Measurements.Clear();
//	product.Measurements.AddRange(
//		request.Measurements.Select(m => new Measurement
//		{
//			Name = m.Name,
//			Price = m.Price
//		})
//	);
//}

//if (request.Colors != null)
//{
//	product.Colors.Clear();
//	product.Colors.AddRange(
//		request.Colors.Select(c => new Color
//		{
//			HexCode = c.HexCode,
//			Price = c.Price,
//			ImagePath = fileService.SaveFile(c.ImagePath, "Images/Products/Colors", [".jpg", ".png", ".JPG", ".PNG", ".jpeg", ".JPEG", ".pdf"])
//		})
//	);
//}

//if (request.Specifications != null && request.Specifications.Count != 0)
//{
//	product.Specifications.Clear();
//	foreach (var specification in request.Specifications)
//	{
//		int specificationId;
//		var specfromdb = await specificationRepository.GetAttributeByName(specification.Name);

//		if (specfromdb != null)
//			specificationId = specfromdb.Id;
//		else
//			specificationId =
//				await specificationRepository.AddAttribute(new Specification { Name = specification.Name });

//		var productSpec = new ProductSpecification
//		{
//			SpecificationId = specificationId,
//			ProductId = product.Id,
//			Value = specification.Value
//		};

//		product.ProductSpecifications.Add(productSpec);
//	}
//}


//foreach(var image in product.Images)
//{
//	string fileName = Path.GetFileName(image.ImagePath);
//	string fullPath = Path.Combine("Images", "Products", fileName).Replace("\\", "/");
//	fileService.DeleteFile(fullPath);
//}