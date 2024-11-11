using MediatR;

namespace Template.Application.ContactUs.Command.SendContactUs
{
	public class SendContactUsCommand : IRequest<int>
	{
		public string Name { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Message { get; set; } = default!;
	}
}
