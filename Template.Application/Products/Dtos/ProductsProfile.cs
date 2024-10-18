using AutoMapper;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos
{
	public class ProductsProfile : Profile
	{
		public ProductsProfile() 
		{
			CreateMap<Product, ProductDto>();
			CreateMap<CreateProductCommand, Product>();
			CreateMap<UpdateProductCommand, Product>();
		}
	}
}
