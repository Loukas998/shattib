using AutoMapper;
using Template.Application.Users.Commands.Register;
using Template.Domain.Entities;

namespace Template.Application.Users.Dtos
{
	public class UsersProfile : Profile
	{
		public UsersProfile()
		{
			CreateMap<RegisterUserCommand, User>();
		}
	}
}
