using AutoMapper;
using Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements;
using Template.Domain.Entities.Orders;
using Template.Application.SpecifiedMeasurements.Commands.UpdateSpecifiedMeasurements;

namespace Template.Application.SpecifiedMeasurements.Dtos
{
	public class SpecifiedMeasurementsProfile : Profile
	{
		public SpecifiedMeasurementsProfile()
		{
			CreateMap<CreateSpecifiedMeasurementsCommand, SpecifiedMeasurement>()
				.ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ImageResolver>());

			CreateMap<UpdateSpecifiedMeasurementsCommand, SpecifiedMeasurement>()
				.ForMember(dest => dest.ImagePath, opt => opt.MapFrom<UpdateImageResolver>())
				.ForAllMembers(opt =>
						opt.Condition((src, dst, srcMember) => srcMember != null));

			CreateMap<SpecifiedMeasurement, SpecifiedMeasurementDto>();
		}
	}
}
