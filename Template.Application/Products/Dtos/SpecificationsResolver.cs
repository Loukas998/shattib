using AutoMapper;
using AutoMapper.Execution;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos
{
	public class SpecificationsResolver : IValueResolver<CreateProductCommand, Product, List<Specification>>
	{
		public List<Specification> Resolve(CreateProductCommand source, Product destination, List<Specification> destMember, ResolutionContext context)
		{
			if(source.Specifications != null)
			{
				List<Specification> specifications = [];
				foreach (var specification in source.Specifications)
				{
					specifications.Add(new Specification { Name = specification.Name });
				}

				return specifications;
			}
			return null;
		}
	}
}
