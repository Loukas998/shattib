using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class ContactUsRepository(TemplateDbContext dbContext) : IContactUsRepository
	{
		public async Task<IEnumerable<ContactUs>> GetAllAsync()
		{
			return await dbContext.Contacts.ToListAsync();
		}

		public async Task<ContactUs?> GetByAsync(int id)
		{
			return await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<int> SendAsync(ContactUs entity)
		{
			dbContext.Contacts.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}
	}
}
