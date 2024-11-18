using MediatR;

namespace Template.Application.Users.Commands.Verify
{
	public class VerifyAccountCommand : IRequest<bool>
	{
		public string OTPCode { get; set; } = default!;
	}
}
