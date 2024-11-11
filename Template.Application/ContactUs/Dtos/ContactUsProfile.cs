using AutoMapper;
using Template.Application.ContactUs.Command.SendContactUs;

namespace Template.Application.ContactUs.Dtos
{
	public class ContactUsProfile : Profile
	{
		public ContactUsProfile()
		{
			CreateMap<SendContactUsCommand, Domain.Entities.ContactUs>();
		}
	}
}
