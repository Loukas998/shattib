using AutoMapper;
using Template.Application.Products.Dtos;
using Template.Application.Specifications.Commands.CreateCommand;
using Template.Application.Specifications.Commands.UpdateCommand;
using Template.Domain.Entities.Products;

namespace Template.Application.Specifications.Dtos
{
    public class SpecificationProfile : Profile
	{
		public SpecificationProfile() 
		{
			CreateMap<CreateSpecificationCommand, Specification>();
			CreateMap<UpdateSpecificationCommand, Specification>();
			CreateMap<SpecificationDto, Specification>();
		}
	}
}
