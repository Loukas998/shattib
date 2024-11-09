using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;
using Template.Application.Statistics;
using Template.Domain.Constants;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

		public async Task<List<MiniProfitsDto>> GetProfitsByDate(int? year, int? month, int? day)
		{
			var query = dbContext.Orders.AsQueryable();
			if (year.HasValue)
			{
				query = query.Where(o => o.DateOfOrder.HasValue && o.DateOfOrder.Value.Year == year.Value);
			}

			if (month.HasValue)
			{
				query = query.Where(o => o.DateOfOrder.HasValue && o.DateOfOrder.Value.Month == month.Value);
			}

			if (day.HasValue)
			{
				query = query.Where(o => o.DateOfOrder.HasValue && o.DateOfOrder.Value.Day == day.Value);
			}

			var result = await query
								   .Join(dbContext.Users,  
									   o => o.UserId,          
									   u => u.Id,              
									   (o, u) => new { o, u }) 
								   .Select(x => new MiniProfitsDto
								   {
									   DateOfOrder = x.o.DateOfOrder!.Value,
									   Username = x.u.UserName!,  
									   TotalPrice = x.o.TotalPrice
								   })
								   .ToListAsync();
			return result;
		}

	}
}
