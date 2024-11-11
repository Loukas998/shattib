namespace Template.Domain.Entities.AuthClasses
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public string Role { get; set; } = default!;
    }
}
