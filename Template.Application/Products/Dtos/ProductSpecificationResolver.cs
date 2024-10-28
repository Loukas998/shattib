using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos
{
	public class ProductSpecificationResolver : IValueResolver<CreateProductCommand, Product, List<ProductSpecification>>
	{
		public List<ProductSpecification> Resolve(CreateProductCommand source, Product destination, List<ProductSpecification> destMember, ResolutionContext context)
		{
			List<ProductSpecification> productSpecifications = [];
			if (source.Specifications != null)
			{
				foreach (var productSpecification in source.Specifications)
				{
					productSpecifications.Add(new ProductSpecification { Value = productSpecification.Value });
				}
				return productSpecifications;
			}
			return productSpecifications;
		}
	}
}
