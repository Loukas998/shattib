using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;


namespace Template.Application.Users.Commands.SendSMS
{
	public class SendSMSCommandHandler(ILogger<SendSMSCommandHandler> logger, IVonageService vonageService)
		: IRequestHandler<SendSMSCommand>
	{
		public async Task Handle(SendSMSCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Sending sms message");
			var otpCode = new Random().Next(100000, 999999).ToString();
			await vonageService.SendSMSAsync(request.DestinationPhoneNumber, otpCode);
		}
	}
}
