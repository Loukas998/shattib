using Template.Domain.Entities.Criterias;

namespace Template.Domain.Repositories;

public interface ICriteriaRepository
{
    public Task<IEnumerable<Criteria>> GetAllAsync(string status);

    public Task<Criteria?> GetByIdAsync(int id);

    public Task<int> CreateCriteriaAsync(Criteria criteria);
    public Task<List<Criteria>> GetAllByUserId(string userId, string status);
    public Task<Criteria?> UpdateCriteriaStatusAsync(int id, string status);
    public Task<Criteria> UpdateCriteriaAsync(Criteria criteria);
}