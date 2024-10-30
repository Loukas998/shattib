using Microsoft.AspNetCore.Identity;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Seeders
{
	internal class RolesSeeder(TemplateDbContext dbContext) : ISeeder
	{
		public async Task Seed()
		{
			if(!dbContext.Roles.Any())
			{
				var roles = GetRoles();
				dbContext.Roles.AddRange(roles);
				await dbContext.SaveChangesAsync();
			}
		}

		private List<IdentityRole> GetRoles()
		{
			List<IdentityRole> roleList = [
				new()
				{
					Name = "Client",
					NormalizedName = "CLIENT"
				},
				new()
				{
					Name = "Business",
					NormalizedName = "BUSINESS"
				},
				new()
				{
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				}
			];
			return roleList;
		}
	}
}
