using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface ISeededValuesRepository
	{
		public Task<List<SubCategory>> GetAllSubCategories();
		public Task<List<Category>> GetAllCategories();
		public List<string> GetOrderKinds();
		public List<string> GetOrderStatuses();
	}	
}
