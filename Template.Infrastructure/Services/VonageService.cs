using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace Template.Infrastructure.Services
{
	public class VonageService(IConfiguration configuration, ILogger<VonageService> logger) : IVonageService
	{
		public async Task SendSMSAsync(string phoneNumber, string otp)
		{
			var vonageClient = new VonageClient(Credentials.FromApiKeyAndSecret(
					configuration["Vonage:ApiKey"],
					configuration["Vonage:ApiSecret"]
				));

			var response = await vonageClient.SmsClient.SendAnSmsAsync(new SendSmsRequest()
			{
				To = phoneNumber,
				From = configuration["Vonage:PhoneNumber"],
				Text = $"Your account verification code at Shattib: {otp}"
			});
			logger.LogInformation("********the message count: {Count}", response.Messages.Count());
			logger.LogInformation("********the message status: {Status}", response.Messages[0].Status);
			logger.LogInformation("********the message price: {Price}", response.Messages[0].MessagePrice);
			logger.LogInformation("********the message error message: {Error}", response.Messages[0].ErrorText);
			logger.LogInformation("********the message networkId: {Network}", response.Messages[0].Network);
		}
	}
}
