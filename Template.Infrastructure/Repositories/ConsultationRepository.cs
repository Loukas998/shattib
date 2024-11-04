using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class ConsultationRepository(TemplateDbContext dbContext) : IConsultationRepository
	{
		public async Task<int> CreateConsultationAsync(Consultation entity)
		{
			dbContext.Consultations.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task DeleteAsync(Consultation entity)
		{
			dbContext.Consultations.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Consultation>> GetAllConsultationsAsync()
		{
			return await dbContext.Consultations.ToListAsync();
		}

		public async Task<Consultation> GetConsultationByIdAsync(int id)
		{
			return await dbContext.Consultations.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task SaveChangesAsync() => await SaveChangesAsync();
	}
}
