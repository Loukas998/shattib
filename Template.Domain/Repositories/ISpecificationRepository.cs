using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface ISpecificationRepository
	{
		public Task<int> AddAttribute(Specification entity);
		public Task AddProductAttribute(List<ProductSpecification> entities);
		public Task DeleteAttribute(Specification entity);
		public Task UpdateAttribute(int id, string newName);
		public Task<IEnumerable<Specification>> GetAllAttributes();
	}
}
