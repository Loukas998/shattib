using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
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

			CreateMap<CreateProductCommand, Product>()
				.ForMember(dest => dest.Images, opt => opt.MapFrom<ImageResolver>())
				.ForMember(dest => dest.Specifications, opt => opt.MapFrom<SpecificationsResolver>());

			CreateMap<UpdateProductCommand, Product>()
				.ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
		}
	}
}
