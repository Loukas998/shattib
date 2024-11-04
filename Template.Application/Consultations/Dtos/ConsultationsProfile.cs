using AutoMapper;
using Template.Application.Consultations.Commands.CreateConsultation;
using Template.Domain.Entities.EngConsultation;

namespace Template.Application.Consultations.Dtos
{
	public class ConsultationsProfile : Profile
	{
		public ConsultationsProfile()
		{
			CreateMap<CreateConsultationCommand, Consultation>();
			CreateMap<ConsultationDto, Consultation>();
		}
	}
}
