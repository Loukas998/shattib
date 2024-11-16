using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;


namespace Template.Application.Users.Commands.SendSMS
{
	public class SendSMSCommandHandler(ILogger<SendSMSCommandHandler> logger, IVonageService vonageService,
		IAccountRepository accountRepository) : IRequestHandler<SendSMSCommand>
	{
		public async Task Handle(SendSMSCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Sending sms message");
			string otpCode = await accountRepository.GenerateOTP(request.DestinationPhoneNumber);
			await vonageService.SendSMSAsync(request.DestinationPhoneNumber, otpCode);
		}
	}
}
