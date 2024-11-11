using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
	public interface IContactUsRepository
	{
		public Task<int> SendAsync(ContactUs entity);
		public Task<IEnumerable<ContactUs>> GetAllAsync();
		public Task<ContactUs?> GetByAsync(int id);
	}
}
