using MediatR;

namespace Template.Application.Users.Commands.DeActivateCode
{
	public class DeActivateCodeCommand : IRequest
	{
		public string Code { get; set; } = default!;
	}
}
