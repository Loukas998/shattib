using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthClasses;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class AccountRepository(TemplateDbContext dbContext, UserManager<User> userManager, 
		IConfiguration configuration) : IAccountRepository
	{
		private readonly string _loginProvidor = "ShattibTokenProvidor";
		private readonly string _refreshToken = "RefreshToken";
		private User? _user;
		private readonly int _expiresInMinutes = Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"]);

		public async Task<string> CreateRefreshToken()
		{
			await userManager.RemoveAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken);
			var newToken = await userManager.GenerateUserTokenAsync(_user!, _loginProvidor, _refreshToken);
			var res = await userManager.SetAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken, newToken);
			return newToken;
		}

		public async Task<AuthResponseDto?> VerifyRefreshToken(RefreshTokenRequest refreshTokenRequest)
		{
			_user = await userManager.FindByIdAsync(refreshTokenRequest.UserId);
			if (_user is null)
			{
				return null;
			}

			var isValidRefreshToken = await userManager.VerifyUserTokenAsync(_user, _loginProvidor, _refreshToken, refreshTokenRequest.RefreshToken);
			if (isValidRefreshToken)
			{
				var roles = await userManager.GetRolesAsync(_user);
				string role = roles.FirstOrDefault()!;
				return new AuthResponseDto
				{
					AccessToken = await GenerateAccessToken(_user),
					RefreshToken = await CreateRefreshToken(),
					DurationInMinutes = _expiresInMinutes,
					Role = role
				};
			}
			return null;
		}

		public async Task<AuthResponseDto?> Login(string email, string password)
		{
			_user = await userManager.FindByEmailAsync(email);
			if (_user == null || !_user.IsActive)
			{
				return null;
			}

			if(await userManager.CheckPasswordAsync(_user, password))
			{
				var roles = await userManager.GetRolesAsync(_user);
				string role = roles.FirstOrDefault()!;
				return new AuthResponseDto
				{
					AccessToken = await GenerateAccessToken(_user),
					RefreshToken = await CreateRefreshToken(),
					DurationInMinutes = _expiresInMinutes,
					Role = role
				};
			}
			return null;
		}

		public async Task<IEnumerable<IdentityError>> Register(User user, string password, string role)
		{
			var existingUser = await userManager.FindByEmailAsync(user.Email!);
			if (existingUser != null)
			{
				return new List<IdentityError>
						{
							new IdentityError { Description = "This email already exists." }
						};
			}
			user.UserName = user.Email;
			var result = await userManager.CreateAsync(user, password);
			if(result.Succeeded)
			{
				await userManager.AddToRoleAsync(user, role);
			}
			return result.Errors;
		}

		private async Task<string> GenerateAccessToken(User user)
		{
			_user = user;
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var roleClaims = await userManager.GetRolesAsync(_user);
			var roles = roleClaims.Select(x => new Claim(ClaimTypes.Role, x));

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, _user.Id!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, _user.Email!),
				new Claim(JwtRegisteredClaimNames.PhoneNumber, _user.PhoneNumber!),
				
			}.Union(roles);

			var token = new JwtSecurityToken(
					issuer: configuration["JwtSettings:Issuer"],
					audience: configuration["JwtSettings:Audience"],
					signingCredentials: credentials,
					claims: claims,
					expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"]))
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task TokenDelete(User user)
		{
			_user = user;
			await userManager.RemoveAuthenticationTokenAsync(_user, _loginProvidor, _refreshToken);
			await userManager.UpdateSecurityStampAsync(_user);
		}

		public async Task<User> GetUserById(string userId)
		{
			return await userManager.FindByIdAsync(userId);
		}

		public async Task<string> GenerateOTP(string userPhoneNumber)
		{
			string otpCode = new Random().Next(100000, 999999).ToString();
			var dbOtp = await dbContext.OneTimePasswords.FirstOrDefaultAsync(o => o.Code == otpCode);
			while(dbOtp != null && dbOtp.Code == otpCode)
			{
				otpCode = new Random().Next(100000, 999999).ToString();
			}
			var ontTimePassword = new OneTimePassword
			{
				Code = otpCode,
				CreatedAt = DateTime.Now,
				ActiveUntil = DateTime.Now.AddMinutes(10),
				PhoneNumber = userPhoneNumber,
				IsActive = true
			};
			dbContext.OneTimePasswords.Add(ontTimePassword);
			await dbContext.SaveChangesAsync();
			return otpCode;
		}

		public async Task<bool> VerifyAccountAsync(string otpCode)
		{
			var dbOtp = await dbContext.OneTimePasswords.FirstOrDefaultAsync(o => o.Code == otpCode);
			if (dbOtp != null && dbOtp.IsActive )
			{
				var user = await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dbOtp.PhoneNumber);
				if(user != null)
				{
					user.IsActive = true;
					dbContext.OneTimePasswords.Remove(dbOtp);
					await dbContext.SaveChangesAsync();
					return true;
				}
				return false;
			}
			return false;
		}

		public async Task DeActivateOTPAsync(string otpCode)
		{
			var otp = await dbContext.OneTimePasswords.FirstOrDefaultAsync(o => o.Code == otpCode);
			if (otp != null)
			{
				otp.IsActive = false;
				dbContext.Remove(otp);
			}
			await dbContext.SaveChangesAsync();
		}
	}
}
