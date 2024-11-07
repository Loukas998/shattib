using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;
using Template.Application.Statistics;
using Template.Domain.Constants;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class StatisticsRepository(TemplateDbContext dbContext) : IStatisticsRepository
	{
		public async Task<int> GetNumberOfBusinessesAsync()
		{
			int count = await (from ur in dbContext.UserRoles
							   join r in dbContext.Roles on ur.RoleId equals r.Id
							   where r.Name == UserRoles.Business
						       select ur.UserId)
							   .Distinct()
							   .CountAsync();
			return count;
		}

		public async Task<int> GetNumberOfClientsAsync()
		{
			int count = await (from ur in dbContext.UserRoles
						       join r in dbContext.Roles on ur.RoleId equals r.Id
						       where r.Name == UserRoles.Client
						       select ur.UserId)
						       .Distinct()
						       .CountAsync();
			return count;
		}

		public async Task<int> GetNumberOfOrdersAsync()
		{
			int count = await dbContext.Orders.CountAsync();
			return count;
		}

		public async Task<int> GetNumberOfProductsAsync()
		{
			int count = await dbContext.Products.CountAsync();
			return count;
		}
	}
}
