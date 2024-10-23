using Template.Domain.Entities.Products;

namespace Template.Domain.Repositories
{
	public interface ISpecificationRepository
	{
		public Task AddAttribute(Specification entity);
		public Task DeleteAttribute(Specification entity);
		public Task UpdateAttribute(int id, string newName);
		public Task GetAllAttributes();
	}
}
