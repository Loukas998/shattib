﻿using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductSpecifications, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(p => p.SubCategory.CategoryId));

        CreateMap<Color, ColorDto>();
        CreateMap<Measurement, MeasurementDto>();

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom<ImagesResolver>())
            .ForMember(dest => dest.Specifications, opt => opt.Ignore())
            .ForMember(dest => dest.Colors, opt => opt.Ignore())
			.ForMember(dest => dest.Measurements, opt => opt.Ignore());

		CreateMap<UpdateProductCommand, Product>()
			.ForMember(dest => dest.Specifications, opt => opt.Ignore())
			.ForMember(dest => dest.Colors, opt => opt.Ignore())
			.ForMember(dest => dest.Measurements, opt => opt.Ignore())
			.ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null)); 

        CreateMap<Product, MiniProductDto>().ReverseMap();

        CreateMap<Category, CatSubCatProductsDto>()
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(sb => sb.SubCategories));

        CreateMap<SubCategory, SubCategoryDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(p => p.Products));
    }
}