using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using System.IO;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Dtos
{
	public class ImageResolver(IFileService fileService) : IValueResolver<CreateProductCommand, Product, List<ProductImages>>
	{
		public List<ProductImages> Resolve(CreateProductCommand source, Product destination, List<ProductImages> destMember, ResolutionContext context)
		{
			List<ProductImages> productImages = [];

			List<string>? paths = fileService.SaveFiles(source.Images, "Images/Products", [".jpg", ".png"]);
			if(paths != null)
			{
				foreach (var path in paths)
				{
					productImages.Add(new ProductImages
					{
						ImagePath = path
					});
				}
			}
			return productImages;
		}
	}
}
