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
			List<Specification> specifications = [];
			if (source.Specifications != null)
			{
				foreach (var specification in source.Specifications)
				{
					specifications.Add(new Specification { Name = specification.Name });
				}

				return specifications;
			}
			return specifications;
		}
	}
}
