using Microsoft.AspNetCore.Http;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface ISpecifiedMeasurementRepository
	{
		public Task<int> CreateAsync(SpecifiedMeasurement entity);
		public Task<SpecifiedMeasurement?> GetByIdAsync(int id);
		public Task<IEnumerable<SpecifiedMeasurement>> GetAllAsync();
		public Task<IEnumerable<SpecifiedMeasurement>> GetAllByUserAsync(string userId);
		public Task DeleteAsync(SpecifiedMeasurement entity);
		public Task SaveChangesAsync();
	}
}
