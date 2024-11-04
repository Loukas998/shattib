using Template.Domain.Entities.Criteria;

namespace Template.Domain.Repositories;

public interface ICriteriaRepository
{
    public Task<IEnumerable<Criteria>> GetAllAsync();

    public Task<Criteria?> GetByIdAsync(int id);

    public Task<int> CreateCriteriaAsync(Criteria criteria);
    public Task<List<Criteria>> GetAllByUserId(string userId);
    public Task<Criteria?> UpdateCriteriaStatusAsync(int id, string status);
}