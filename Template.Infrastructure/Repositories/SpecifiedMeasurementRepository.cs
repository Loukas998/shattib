using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class SpecifiedMeasurementRepository(TemplateDbContext dbContext) : ISpecifiedMeasurementRepository
	{
		public async Task<int> CreateAsync(SpecifiedMeasurement entity)
		{
			dbContext.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task DeleteAsync(SpecifiedMeasurement entity)
		{
			dbContext.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<SpecifiedMeasurement>> GetAllAsync()
		{
			return await dbContext.SpecifiedMeasurements.ToListAsync();
		}

		public async Task<IEnumerable<SpecifiedMeasurement>> GetAllByUserAsync(string userId)
		{
			return await dbContext.SpecifiedMeasurements.Where(sm => sm.UserId == userId).ToListAsync();
		}

		public async Task<SpecifiedMeasurement?> GetByIdAsync(int id)
		{
			var specifiedMeasurement = await dbContext.SpecifiedMeasurements
				.FirstOrDefaultAsync(sm => sm.Id == id);
			return specifiedMeasurement;
		}

		public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
	}
}
