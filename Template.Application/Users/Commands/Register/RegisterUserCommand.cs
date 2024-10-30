using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Template.Application.Users.Commands.Register
{
	public class RegisterUserCommand : IRequest<IEnumerable<IdentityError>>
	{
		public string UserName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
		public string Role { get; set; } = default!;
	}
}
