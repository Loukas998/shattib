using AutoMapper;
using Template.Application.Consultations.Commands.CreateConsultation;
using Template.Application.Consultations.Commands.UpdateConsultation;
using Template.Application.Products.Commands.UpdateProductCommand;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Entities.Products;

namespace Template.Application.Consultations.Dtos
{
	public class ConsultationsProfile : Profile
	{
		public ConsultationsProfile()
		{
			CreateMap<CreateConsultationCommand, Consultation>();

			CreateMap<Consultation, ConsultationDto>();

			CreateMap<UpdateConsultationCommand, Consultation>()
			.ForAllMembers(opt =>
				opt.Condition((src, dst, srcMember) => srcMember != null));
		}
	}
}
