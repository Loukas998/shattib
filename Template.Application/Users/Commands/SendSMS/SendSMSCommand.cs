using MediatR;

namespace Template.Application.Users.Commands.SendSMS
{
	public class SendSMSCommand : IRequest
	{
		public string DestinationPhoneNumber { get; set; } = default!;
	}
}
