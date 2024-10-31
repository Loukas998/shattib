using MediatR;
using Template.Domain.Entities.AuthClasses;

namespace Template.Application.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<AuthResponseDto?>
	{
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
