using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface ISeededValuesRepository
	{
		public Task<List<SubCategory>> GetAllSubCategories();
		public Task<List<Category>> GetAllCategories();
		public Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
		public List<string> GetOrderKinds();
		public List<string> GetOrderStatuses();
		public List<string> GetConsultationStatuses();
	}	
}
