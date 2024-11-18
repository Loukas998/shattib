using AutoMapper;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Dtos
{
	public class UpdateImagesResolver(IFileService fileService)
		: IValueResolver<UpdateProductCommand, Product, List<ProductImages>>
	{
		public List<ProductImages> Resolve(UpdateProductCommand source, Product destination,
			List<ProductImages> destMember, ResolutionContext context)
		{
			List<ProductImages> productImages = [];
			if (source.Images == null) return productImages;

			var paths = fileService.SaveFiles(source.Images, "Images/Products", [".jpg", ".png", ".jpeg"]);
			if (paths != null) productImages.AddRange(paths.Select(path => new ProductImages { ImagePath = path }));
			return productImages;
		}
	}
}
