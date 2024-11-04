using Template.Domain.Entities.EngConsultation;

namespace Template.Domain.Repositories
{
	public interface IConsultationRepository
	{
		public Task<int> CreateConsultationAsync(Consultation entity);
		public Task DeleteAsync(Consultation entity);
		public Task<Consultation> GetConsultationByIdAsync(int id);
		public Task<IEnumerable<Consultation>> GetAllConsultationsAsync();
		public Task SaveChangesAsync();
	}
}
