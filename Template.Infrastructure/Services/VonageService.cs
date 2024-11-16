using Azure.Core;
using Microsoft.Extensions.Configuration;
using Template.Domain.Repositories;
using Vonage;
using Vonage.Request;

namespace Template.Infrastructure.Services
{
	public class VonageService(IConfiguration configuration) : IVonageService
	{
		public async Task SendSMSAsync(string phoneNumber, string otp)
		{
			var vonageClient = new VonageClient(Credentials.FromApiKeyAndSecret(
					configuration["Vonage:ApiKey"],
					configuration["Vonage:ApiSecret"]
				));

			await vonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
			{
				To = phoneNumber,
				From = configuration["Vonage:PhoneNumber"],
				Text = $"Your account verification code at Shattib: {otp}"
			});
		}
	}
}
