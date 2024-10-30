using MediatR;
using Template.Domain.Entities.AuthClasses;

namespace Template.Application.Users.Commands.RefreshToken
{
	public class RefreshTokenCommand : IRequest<AuthResponseDto?>
	{
		public string RefreshToken { get; set; } = default!;
	}
}
