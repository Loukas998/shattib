namespace Template.Domain.Repositories
{
	public interface IVonageService
	{
		public Task SendSMSAsync(string phoneNumber, string otp);
	}
}
