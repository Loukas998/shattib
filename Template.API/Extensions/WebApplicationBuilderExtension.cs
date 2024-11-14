using Azure.AI.Translation.Text;
using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Template.Domain.Entities;
using Template.API.Middlewares;

namespace Template.API.Extensions
{
	public static class WebApplicationBuilderExtension
	{
		private static readonly string _key = "FqyfAN1ywtRSCX0QB42HgCDMLzUuIUpq0EH9WiAf1wxqpUBuoFp0JQQJ99AKACF24PCXJ3w3AAAbACOGGFpp";
		private static readonly string _endpoint = "https://api.cognitive.microsofttranslator.com/";
		public static void AddPresentation(this WebApplicationBuilder builder)
		{
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					ValidAudience = builder.Configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
				};
			}
			);

			

			//builder.Services.AddSingleton
			//	(new TextTranslationClient(new AzureKeyCredential(_key),
			//		new Uri(_endpoint), "uaenorth"));


			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
						},
						[]
					}
				});
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddIdentityApiEndpoints<User>();
		}
	}
}
