using Microsoft.EntityFrameworkCore;
using Template.Domain.Constants;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class SeededValuesRepository(TemplateDbContext dbContext) : ISeededValuesRepository
	{
		public async Task<List<Category>> GetAllCategories() => await dbContext.Categories.ToListAsync();

		public async Task<List<SubCategory>> GetAllSubCategories() => await dbContext.Subcategories.ToListAsync();

		public List<string> GetOrderKinds()
		{
			List<string> orderKinds = [OrderConstants.Order, OrderConstants.Sample];
			return orderKinds;
		}

		public List<string> GetOrderStatuses()
		{
			List<string> orderStatuses = [OrderConstants.Pending, OrderConstants.Accepted, OrderConstants.Rejected];
			return orderStatuses;
		}

	}
}
