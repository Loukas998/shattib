using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthClasses;

namespace Template.Domain.Repositories
{
    public interface IAccountRepository
	{
		public Task<IEnumerable<IdentityError>> Register(User user, string password, string role);
		public Task<AuthResponseDto?> Login(string email, string password);
		public Task<string> CreateRefreshToken();
		public Task<AuthResponseDto?> VerifyRefreshToken(RefreshTokenRequest refreshTokenRequest);
		public Task TokenDelete(User user);
		public  Task<User> GetUserById(string userId);
	}
}
