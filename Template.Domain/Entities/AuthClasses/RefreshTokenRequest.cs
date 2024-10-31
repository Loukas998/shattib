namespace Template.Domain.Entities.AuthClasses
{
	public class RefreshTokenRequest
	{
		public string UserId { get; set; } = default!;
		public string RefreshToken { get; set; } = default!;
	}
}
